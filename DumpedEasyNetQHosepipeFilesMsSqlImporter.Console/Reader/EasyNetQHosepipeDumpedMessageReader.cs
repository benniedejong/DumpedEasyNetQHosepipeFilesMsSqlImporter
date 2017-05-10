namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Reader
{
    using Entities;
    using Newtonsoft.Json;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ValueObjects;

    public class EasyNetQHosepipeDumpedMessageReader
    {
        internal enum DumpedFileType
        {
            info,
            message,
            properties
        }
        
        private static Logger Log = LogManager.GetCurrentClassLogger();

        public IEnumerable<EasyNetQHosepipeDumpedFilesInfo> EnumerateDumpedFilesInfos(string easyNetQHosepipeDumpFileDirectory, string rabbitMqQueueName)
        {
            IEnumerable<EasyNetQHosepipeDumpedFilesInfo> dumpedMessageFilesInfos = null;
            try
            {
                Log.Info($"Scanning directory '{easyNetQHosepipeDumpFileDirectory}' for dumped '{rabbitMqQueueName}' messages");
                var fileInfos = new DirectoryInfo(easyNetQHosepipeDumpFileDirectory).GetFiles($"{rabbitMqQueueName}.*.*.txt", SearchOption.TopDirectoryOnly);
                Log.Info($"Found {fileInfos.Count()} files");

                dumpedMessageFilesInfos = fileInfos
                    .Select(x =>
                    {
                        var tmp = x.Name.Split('.');
                        var dumpedFileType = (DumpedFileType)Enum.Parse(typeof(DumpedFileType), tmp.ElementAt(2));
                        return new
                        {
                            FileInfo = x,
                            Index = int.Parse(tmp.ElementAt(1)),
                            Type = dumpedFileType
                        };
                    })
                    .GroupBy(x => x.Index)
                    .Select(x => new EasyNetQHosepipeDumpedFilesInfo
                    {
                        Index = x.First().Index,
                        Info = x.SingleOrDefault(y => y.Type == DumpedFileType.info).FileInfo,
                        Message = x.SingleOrDefault(y => y.Type == DumpedFileType.message).FileInfo,
                        Properties = x.SingleOrDefault(y => y.Type == DumpedFileType.properties).FileInfo
                    })
                    .ToList();

                Log.Info($"Containing {dumpedMessageFilesInfos.Count()} dumped EasyNetQ Hosepipe messages");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Enumerating EasyNetQ Hosepipe messages failed for directory '{easyNetQHosepipeDumpFileDirectory}' and RabbitMQ Queue '{rabbitMqQueueName}'");
                throw ex;
            }

            return dumpedMessageFilesInfos;
        }

        public EasyNetQHosepipeDumped ReadDumpedFilesInfo(EasyNetQHosepipeDumpedFilesInfo dumpedFilesInfo)
        {
            Log.Info($"Reading dumped message with index {dumpedFilesInfo.Index}");

            Log.Info($"Reading info file: '{dumpedFilesInfo.Info.FullName}'");
            var info = File.ReadAllText(dumpedFilesInfo.Info.FullName);
            
            Log.Info($"Reading properties file: '{dumpedFilesInfo.Properties.FullName}'");
            var properties = File.ReadAllText(dumpedFilesInfo.Properties.FullName);

            Log.Info($"Reading message file: '{dumpedFilesInfo.Message.FullName}'");
            var message = File.ReadAllText(dumpedFilesInfo.Message.FullName);

            Log.Info("Constructing HosepipeMessage");

            var easyNetQHosepipeDumped = new EasyNetQHosepipeDumped
            {
                InfoFilePath = dumpedFilesInfo.Info.FullName,
                RawInfo = info,
                PropertiesFilePath = dumpedFilesInfo.Properties.FullName,
                RawProperties = properties,
                MessageFilePath = dumpedFilesInfo.Message.FullName,
                RawMessage = message
            };
            
            easyNetQHosepipeDumped.Properties = JsonConvert.DeserializeObject<EasyNetQHosepipeDumpedProperties>(properties);
            easyNetQHosepipeDumped.Info = JsonConvert.DeserializeObject<EasyNetQHosepipeDumpedInfo>(info);

            return easyNetQHosepipeDumped;
        }
    }
}

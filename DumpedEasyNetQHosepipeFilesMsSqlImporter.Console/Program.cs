namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console
{
    using Model;
    using NLog;
    using Reader;
    using StartupParameters;
    using System;
    using System.Text;

    public class Program
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        private readonly ArgumentsParser argumentsParser;

        private static StringBuilder argumentsResults = new StringBuilder();

        private static bool argumentsSucceeded = true;

        private static Func<string, Action> argumentsMessage = m => () =>
        {
            argumentsResults.AppendLine(m);
            argumentsSucceeded = false;
        };

        public static void Main(string[] args)
        {
            var argumentsParser = new ArgumentsParser();

            // poor man's dependency injection FTW ;)            
            var program = new Program(argumentsParser);
            program.Start(args);
        }

        public Program(ArgumentsParser argumentsParser)
        {
            this.argumentsParser = argumentsParser;
        }

        public void Start(string[] args)
        {
            var arguments = argumentsParser.Parse(args);

            var parameters = new Parameters();
            arguments.WithKey("d", a => parameters.EasyNetQHosepipeDumpFileDirectory = a.Value);
            arguments.WithKey("c", a => parameters.ConnectionString = a.Value);
            arguments.WithKey("q", a => parameters.RabbitMqQueueName = a.Value);

            arguments.At(0, "?", PrintUsage);

            // print usage if there are no arguments
            arguments.At(0, a => { }).FailWith(argumentsMessage("No arguments supplied"));

            if (!argumentsSucceeded)
            {
                Log.Error("Operation failed");
                Log.Error(argumentsResults.ToString());
                Log.Error(string.Empty);
                PrintUsage();
            }
            else
            {
                this.ImportDumpedEasyNetQHosepipeFilesInMsSqlServer(parameters);
            }
        }

        private void ImportDumpedEasyNetQHosepipeFilesInMsSqlServer(Parameters parameters)
        {
            Log.Info($"Going to perform a EasyNetQ Hosepipe dump files to MsSql import.");
            Log.Info($"Using source directory '{parameters.EasyNetQHosepipeDumpFileDirectory}'");
            Log.Info($"Using RabbitMQ queue name '{parameters.RabbitMqQueueName}'");
            Log.Info($"Using database connection string '{parameters.ConnectionString}'");
            var reader = new EasyNetQHosepipeDumpedMessageReader();
            var dumpedFilesInfos = reader.EnumerateDumpedFilesInfos(parameters.EasyNetQHosepipeDumpFileDirectory, parameters.RabbitMqQueueName);
            using (var db = new DatabaseContext(parameters.ConnectionString))
            {
                foreach (var dumpedFilesInfo in dumpedFilesInfos)
                {
                    Log.Info(string.Empty);
                    var message = reader.ReadDumpedFilesInfo(dumpedFilesInfo);
                    Log.Info("Persisting message to database");
                    db.HosepipeDumps.Add(message);
                    Log.Info("Saving changes to database");
                    db.SaveChanges();
                }
            }
        }

        public static void PrintUsage()
        {
            // fixme todo
            Log.Info("PrintUsage");
            //using (var manifest = Assembly.GetExecutingAssembly().GetManifestResourceStream("EasyNetQ.Hosepipe.Usage.txt"))
            //{
            //    if (manifest == null)
            //    {
            //        throw new Exception("Could not load usage");
            //    }
            //    using (var reader = new StreamReader(manifest))
            //    {
            //        Console.Write(reader.ReadToEnd());
            //    }
            //}
        }
    }
}

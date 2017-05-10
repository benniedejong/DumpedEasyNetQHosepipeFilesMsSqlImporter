namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.ValueObjects
{
    using System.IO;

    public class EasyNetQHosepipeDumpedFilesInfo
    {
        public int Index { get; set; }

        public FileInfo Info { get; set; }

        public FileInfo Message { get; set; }

        public FileInfo Properties { get; set; }
    }
}

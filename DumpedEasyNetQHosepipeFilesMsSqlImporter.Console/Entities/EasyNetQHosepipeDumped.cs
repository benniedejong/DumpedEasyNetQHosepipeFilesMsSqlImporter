namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Entities
{
    public class EasyNetQHosepipeDumped
    {
        public int Id { get; set; }

        public virtual EasyNetQHosepipeDumpedProperties Properties { get; set; }

        public virtual EasyNetQHosepipeDumpedInfo Info { get; set; }

        public string MessageFilePath { get; set; }

        public string RawMessage { get; set; }

        public string PropertiesFilePath { get; set; }

        public string RawProperties { get; set; }

        public string InfoFilePath { get; set; }

        public string RawInfo { get; set; }
    }
}

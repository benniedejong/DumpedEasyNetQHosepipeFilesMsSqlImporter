namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.StartupParameters
{
    public sealed class Argument
    {
        public string Value { get; private set; }

        public string Key { get; private set; }

        public bool HasKey { get; private set; }

        public Argument(string value, string key)
        {
            this.Value = value;
            this.Key = key;
            this.HasKey = true;
        }

        public Argument(string value)
        {
            this.Value = value;
            this.HasKey = false;
        }
    }
}
namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Entities
{
    public class EasyNetQHosepipeDumpedInfo
    {
        public int Id { get; set; }

        public string ConsumerTag { get; set; }

        public long DeliverTag { get; set; }

        public bool Redelivered { get; set; }

        public string Exchange { get; set; }

        public string RoutingKey { get; set; }

        public string Queue { get; set; }
    }
}

namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console
{
    using System;

    public class Parameters
    {
        public string EasyNetQHosepipeDumpFileDirectory { get; set; }

        public string RabbitMqQueueName { get; set; }

        public string ConnectionString { get; set; }

        public Parameters()
        {
            // set some defaults
            this.EasyNetQHosepipeDumpFileDirectory = Environment.CurrentDirectory;
            this.RabbitMqQueueName = "EasyNetQ_Default_Error_Queue";
            this.ConnectionString = null;
        }
    }
}

namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console
{
    using System;

    public class Parameters
    {
        public const string EASYNETQ_DEFAULT_ERROR_QUEUE = "EasyNetQ_Default_Error_Queue";

        public string EasyNetQHosepipeDumpFileDirectory { get; set; }

        public string RabbitMqQueueName { get; set; }

        public string ConnectionString { get; set; }

        public Parameters()
        {
            // set some defaults
            this.EasyNetQHosepipeDumpFileDirectory = Environment.CurrentDirectory;
            this.RabbitMqQueueName = EASYNETQ_DEFAULT_ERROR_QUEUE;
            this.ConnectionString = null;
        }
    }
}

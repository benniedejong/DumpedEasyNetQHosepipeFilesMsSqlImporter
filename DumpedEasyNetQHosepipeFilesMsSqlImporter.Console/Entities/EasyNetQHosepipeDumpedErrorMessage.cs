﻿namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EasyNetQHosepipeDumpedErrorMessage
    {
        public EasyNetQHosepipeDumpedErrorMessage()
        {
            this.Headers = new Dictionary<string, object>();
        }

        public int Id { get; set; }

        public string RoutingKey { get; set; }

        public string Exchange { get; set; }

        public string Queue { get; set; }

        public string Exception { get; set; }

        public string Message { get; set; }

        public DateTime DateTime { get; set; }
        
        public string ContentType { get; set; }

        public string ContentEncoding { get; set; }

        public string HeadersDb
        {
            get
            {
                var headers = new StringBuilder();
                if (this.Headers != null)
                {
                    foreach (var key in this.Headers.Keys)
                    {
                        headers.Append($"|{key}:{this.Headers[key].ToString()}|");
                    }
                }

                return headers.ToString();
            }
            set
            {

            }
        }

        public IDictionary<string, object> Headers { get; set; }
        
        public byte DeliveryMode { get; set; }

        public byte Priority { get; set; }

        public string CorrelationId { get; set; }

        public string ReplyTo { get; set; }

        public string Expiration { get; set; }

        public string MessageId { get; set; }

        public long Timestamp { get; set; }

        public string Type { get; set; }

        public string UserId { get; set; }

        public string AppId { get; set; }

        public string ClusterId { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SalesforceCDCSubscriber
{
    public class CDCResponse
    {
        public Data data { get; set; }
        public string channel { get; set; }
    }
    public class Data
    {
        public string schema { get; set; }

        [JsonProperty(PropertyName = "event")]
        public Event _event { get; set; }
        public Payload payload { get; set; }
    }
    public class Event
    {
        public int replayId { get; set; }
    }
    public class Payload
    {
        public DateTime LastModifiedDate { get; set; }
        public string Name { get; set; }
        public ChangeEventHeader ChangeEventHeader { get; set; }
    }

    public class ChangeEventHeader
    {
        public bool isTransactionEnd { get; set; }
        public long commitNumber { get; set; }
        public string commitUser { get; set; }
        public int sequenceNumber { get; set; }
        public string entityName { get; set; }
        public string changeType { get; set; }
        public string changeOrigin { get; set; }
        public string transactionKey { get; set; }
        public string commitTimestamp { get; set; }
        public List<string> recordIds { get; set; }
    }
}

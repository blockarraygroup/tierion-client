using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tierion.Client
{
    public class RecordDetail<TDetail>
    {
        public string id { get; set; }
        public int accountId { get; set; }
        public int datastoreId { get; set; }
        public string status { get; set; }
        public TDetail data { get; set; }
        public string json { get; set; }
        public string sha256 { get; set; }
        [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime timestamp { get; set; }
        public string blockchain_receipt { get; set; }
        public string insights { get; set; }

    }
}
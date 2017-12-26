using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tierion.Client
{
    public class RecordSet
    {
        public string accountId { get; set; }
        public string datastoreId { get; set; }
        public int page { get; set; }
        public int pageCount { get; set; }
        public int pageSize { get; set; }
        public int recordCount { get; set; }
        [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime startDate { get; set; }
        [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime endDate { get; set; }
        public Record[] records { get; set; }
    }
}
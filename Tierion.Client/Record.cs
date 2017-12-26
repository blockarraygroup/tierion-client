using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tierion.Client
{
    public class Record
    {
        public string id { get; set; }
        public string label { get; set; }

        [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime timestamp { get; set; }
    }
}
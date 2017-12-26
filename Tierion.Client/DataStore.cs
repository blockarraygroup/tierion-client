using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tierion.Client
{
    public class DataStore
    {
        //id A unique numeric identifier for the datastore within the system.
        //    key A unique identifier for the datastore used when submitting from HTML forms.
        //    name    The name of the datastore.
        //    groupName The name of the group of which this datastore is a member.
        //    redirectEnabled A boolean indicating whether or not the custom redirect URL is enabled.
        //    redirectUrl The URL a user will be redirected to when submitting data from an HTML Form.
        //    emailNotificationEnabled A boolean indicating whether or not the email notification is enabled.
        //    emailNotificationAddress The recipient email address.
        //    postDataEnabled A boolean indicating whether or not the POST data URL is enabled.
        //    postDataUrl The URL that new record data will be POSTed to when received.
        //    postReceiptEnabled  A boolean indicating whether or not the POST receipt URL is enabled.
        //    postReceiptUrl The URL that the blockchain receipt data will be POSTed to when generated.
        //    timestamp The number of seconds elapsed since epoch when this Datastore was created.

        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string groupName { get; set; }
        public string redirectEnabled { get; set; }
        public string redirectUrl { get; set; }
        public string emailNotificationEnabled { get; set; }
        public string emailNotificationAddress { get; set; }
        public string postDataEnabled { get; set; }
        public string postDataUrl { get; set; }
        public string postReceiptEnabled { get; set; }
        public string postReceiptUrl { get; set; }
        [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime timestamp { get; set; }

    }
}
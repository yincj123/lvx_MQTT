using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Apicall
{
    class Lvx_Info
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("connection_device_name")]
        public string Device_name { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("heath")]
        public string Heath { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("attach")]
        public string Attach { get; set; }



    }
}

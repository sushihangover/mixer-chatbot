using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mixer.Chat.Models
{
    public class HttpError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDesciption { get; set; }
    }
}

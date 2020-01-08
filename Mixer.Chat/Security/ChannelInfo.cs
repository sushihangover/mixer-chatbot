using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mixer.Chat.Security
{
    public class ChannelInfo
    {
        [JsonProperty("roles")]
        public string[] Roles { get; set; }
        
        [JsonProperty("authkey")]
        public string AuthKey { get; set; }

        [JsonProperty("permissions")]
        public string[] Permissions { get; set; }

        [JsonProperty("endpoints")]
        public string[] EndPoints { get; set; }

        [JsonProperty("isLoadShed")]
        public Boolean IsLoadShed { get; set; }

    }
}

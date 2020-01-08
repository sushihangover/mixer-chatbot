using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsWelcomeEvent
	{
		[JsonProperty("server")]
		public string Server { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsUserTimeoutEvent
	{
		[JsonProperty("users")]
		public WsUser User { get; set; }

		[JsonProperty("duration")]
		public Int64 Duration { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


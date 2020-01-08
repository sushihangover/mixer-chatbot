using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsUserLeaveEvent
	{
		[JsonProperty("originatingChannel")]
		public Int64 OriginatingChannel { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("id")]
		public Int64 UserId { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


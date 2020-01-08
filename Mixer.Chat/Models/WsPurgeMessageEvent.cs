using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsPurgeMessageEvent
	{
		[JsonProperty("moderator")]
		public WsModerator Moderator { get; set; }
		[JsonProperty("user_id")]
		public Int64 UserId { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


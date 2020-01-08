using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsDeleteMessageEvent
	{
		[JsonProperty("moderator")]
		public WsModerator Moderator { get; set; }
		[JsonProperty("id")]
		public string MessageId { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


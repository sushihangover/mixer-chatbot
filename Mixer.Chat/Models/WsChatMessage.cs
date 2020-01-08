using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsChatMessage
	{
		[JsonProperty("message")]
		public WsChatMessageItem[] MessageItem { get; set; }

		[JsonProperty("meta")]
		public WsChatMessageMeta Meta { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}
}

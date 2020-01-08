using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsChatMessageItem
	{
		[JsonProperty("type")]
		public ChatMessageType MessageType { get; set; }
		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public string Data { get; set; }
		[JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text { get; set; }
		[JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
		public string Source { get; set; }
		[JsonProperty("pack", NullValueHandling = NullValueHandling.Ignore)]
		public string Pack { get; set; }
		[JsonProperty("coords", NullValueHandling = NullValueHandling.Ignore)]
		public WsChatMessageCoordinate Coords { get; set; }
		[JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
		public string Url { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}
}

using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsClearMessagesEvent
	{
		[JsonProperty("clearer")]
		public WsClearer Clearer { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


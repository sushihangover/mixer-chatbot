using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsChatMessageMeta
	{
		[JsonProperty("whisper", NullValueHandling = NullValueHandling.Ignore)]
		public Boolean Whisper { get; set; }

		[JsonProperty("me", NullValueHandling = NullValueHandling.Ignore)]
		public Boolean Me { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}
}

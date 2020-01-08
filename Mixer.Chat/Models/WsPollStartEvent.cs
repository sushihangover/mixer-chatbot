using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mixer.Chat.Models
{
	public class WsPollStartEvent
	{
		[JsonProperty("originatingChannel")]
		public Int64 OriginatingChannel { get; set; }

		[JsonProperty("q")]
		public string Question { get; set; }

		[JsonProperty("answers")]
		public string[] Answers { get; set; }

		[JsonProperty("author")]
		public WsPollAuthor Author { get; set; }

		[JsonProperty("duration")]
		public Int64 Duration { get; set; }

		[JsonProperty("endsAt")]
		public Int64 EndsAt { get; set; }

		[JsonProperty("voters")]
		public Int64 Voters { get; set; }

		[JsonProperty("responses")]
		public Dictionary<string, Int64> Responses { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


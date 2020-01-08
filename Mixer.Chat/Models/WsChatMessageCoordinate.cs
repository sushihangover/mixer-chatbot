using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsChatMessageCoordinate
	{
		[JsonProperty("x")]
		public Int32 X { get; set; }
		[JsonProperty("y")]
		public Int32 Y { get; set; }
		[JsonProperty("width")]
		public Int32 Width { get; set; }
		[JsonProperty("height")]
		public Int32 Height { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}
}

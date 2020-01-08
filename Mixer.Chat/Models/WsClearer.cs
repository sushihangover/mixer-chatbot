using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsClearer
	{
		[JsonProperty("user_name")]
		public string Username { get; set; }
		[JsonProperty("user_id")]
		public Int64 UserId { get; set; }
		[JsonProperty("user_roles")]
		public string[] UserRoles { get; set; }
		[JsonProperty("user_level")]
		public Int32 UserLevel { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


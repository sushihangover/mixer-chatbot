using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsUserUpdateEvent
	{
		[JsonProperty("user")]
		public Int64 UserId { get; set; }
		[JsonProperty("roles")]
		public string[] Roles { get; set; }
		[JsonProperty("permissions")]
		public string[] Permissions { get; set; }
		[JsonProperty("username")]
		public string Username { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


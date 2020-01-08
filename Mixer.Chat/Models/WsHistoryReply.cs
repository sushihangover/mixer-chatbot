using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mixer.Chat.Models
{
	public class WsHistoryReply
	{
		[JsonProperty("channel")]
		public Int64 Channel { get; set; }
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("user_name")]
		public string Username { get; set; }
		[JsonProperty("user_id")]
		public Int64 UserId { get; set; }
		[JsonProperty("user_roles")]
		public string[] UserRoles { get; set; }

		[JsonProperty("message")]
		public WsChatMessage Message { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}
}

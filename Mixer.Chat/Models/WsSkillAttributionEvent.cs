using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsSkillAttributionEvent
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("skill")]
		public WsSkill Skill { get; set; }
		[JsonProperty("user_name")]
		public string Username { get; set; }
		[JsonProperty("user_id")]
		public Int64 UserId { get; set; }
		[JsonProperty("user_roles")]
		public string UserRoles { get; set; }
		[JsonProperty("user_level")]
		public Int64 UserLevel { get; set; }
		[JsonProperty("user_avatar")]
		public string UserAvatar { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


using Newtonsoft.Json;
using System;

namespace Mixer.Chat.Models
{
	public class WsSkill
	{
		[JsonProperty("skill_id")]
		public string SkillId { get; set; }
		[JsonProperty("skill_name")]
		public string SkillName { get; set; }
		[JsonProperty("execution_id")]
		public string ExecutionId { get; set; }
		[JsonProperty("icon_url")]
		public string IconUrl { get; set; }
		[JsonProperty("cost")]
		public Int32 Cost { get; set; }
		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("message")]
		public WsChatMessage Message { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


using Newtonsoft.Json;
using System;
using System.Text;

namespace Mixer.Chat.Models
{

	public class WsDeleteSkillAttributionEvent
	{
		[JsonProperty("moderator")]
		public WsModerator Moderator { get; set; }
		[JsonProperty("execution_id")]
		public string ExecutionId { get; set; }

		public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
	}

}


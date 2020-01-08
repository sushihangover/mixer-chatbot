using System;
using System.Collections.Generic;
using System.Text;

namespace Mixer.Chat.Models
{
    internal class MessageEventArgs : EventArgs
    {
        public WsMessage Message { get; set; }
        public string Messagestring { get; set; }
        public Int32 MessageId { get; set; }
    }
}

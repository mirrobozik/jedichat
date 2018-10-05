using System;

namespace JediChat.Server.Models
{
    public class ChatMessage
    {
        public string Id { get; set; }

        public DateTime SentUTC { get; set; }

        public string FromUuid { get; set; }

        public string ToUuid { get; set; }

        public string Body { get; set; }
    }
}
using System.Collections.Generic;

namespace JediChat.Server.Models
{
    public class UserInfo
    {
        public string Uuid { get; set; }

        public HashSet<string> ConnectionIds { get; set; }
    }
}
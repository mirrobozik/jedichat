using System.Collections.Generic;

namespace JediChat.Server.Models
{
    public class GroupChangeEvent
    {
        public ICollection<string> Groups { get; set; }
    }
}
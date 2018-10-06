namespace JediChat.Server.Models
{
    public class PresenceEvent : Event
    {        
        public string Action { get; set; }

        public string Group { get; set; }

        public string Uuid { get; set; }
    }

    public static class PresenceAction
    {
        public const string Join = "join";
        public const string Leave = "leave";
    }
}
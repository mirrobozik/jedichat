namespace JediChat.Server.Models
{
    public class MessageOutputEvent
    {
        //public string Id { get; set; }

        public string Sender { get; set; }

        public string Group { get; set; }

        public string Message { get; set; }
    }
}
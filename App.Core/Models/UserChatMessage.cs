namespace App.Core.Models
{
    public class UserChatMessage
    {
        public string SenderName { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
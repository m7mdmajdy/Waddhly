using Waddhly.Models;

namespace Waddhly.DTOs
{
    public class MessageDTO
    {
        public string Content { get; set; }
        public DateTime? Date { get; set; }
        public string SenderId { get; set; }
        public string senderName { get; set; }
        public string RecieverId { get; set; }
        public string RecieverName { get; set; }
    }
}

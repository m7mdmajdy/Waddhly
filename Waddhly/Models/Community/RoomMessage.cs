namespace Waddhly.Models.Community
{
    public class RoomMessage
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime? Date { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Reciever { get; set; }
    }
}

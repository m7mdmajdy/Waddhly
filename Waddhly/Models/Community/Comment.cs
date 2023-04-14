namespace Waddhly.Models.Community
{
    public class Comment
    {
        public int ID { get; set; }
        public string Description{ get; set; }
        public virtual User user { get; set; }
        public virtual Post post { get; set; }

    }
}

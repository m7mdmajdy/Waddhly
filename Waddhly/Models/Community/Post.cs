namespace Waddhly.Models.Community
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual User user { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

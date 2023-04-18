using System.ComponentModel.DataAnnotations.Schema;
using Waddhly.Data;
namespace Waddhly.Models.Community
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(user))]
        public string userid { get; set; }
        public virtual User user { get; set; }
        public List<Comment> Comments { get; set; }
    }
}











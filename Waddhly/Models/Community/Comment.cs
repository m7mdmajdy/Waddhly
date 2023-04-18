using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations.Schema;

namespace Waddhly.Models.Community
{
    public class Comment
    {


        public Comment() { }
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("post")]
        public int postid { get; set; }
        [ForeignKey("user")]
        public string userid { get; set; }
        public virtual User user { get; set; }

        public virtual Post post { get; set; }


    }
}

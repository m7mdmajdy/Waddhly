using System.ComponentModel.DataAnnotations.Schema;

namespace Waddhly.Models.Community
{
    public class Rooms
    {
        public int id { get; set; }
        public virtual User user1 { get; set; }
        public virtual User user2 { get; set; }
    }
}

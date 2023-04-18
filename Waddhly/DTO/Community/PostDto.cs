using System.ComponentModel.DataAnnotations.Schema;
using Waddhly.Models;

namespace Waddhly.DTO.Community
{
    public class PostDto

    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string userid { get; set; }
    }
}

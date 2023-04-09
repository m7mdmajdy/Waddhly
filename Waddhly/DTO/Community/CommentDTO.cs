namespace Waddhly.DTO.Community
{
    public class CommentDTO
    {
        public int postid { get; set; }
        public string postTitle { get; set; }
        public List<Tuple<string, string>> comments { get; set; } = new List<Tuple<string, string>>();


    }
}

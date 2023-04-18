namespace Waddhly.DTO.Community
{
    public class CommentDTO
    {
        public int postid { get; set; }
        public string postTitle { get; set; }
        public string postuserid { get; set; }
        public string postusername { get; set; }
        public DateTime postDate { get; set; }
        public string postDescription { get; set; }
        public byte[] userimage { get; set; }
        public string commentdescription { get; set; }
        public DateTime commentdate { get; set; }
        public int commentid { get; set; }
        public string commentUserId { get; set; }

        public List<Tuple<string, string, string, DateTime, byte[]>> comments { get; set; } = new List<Tuple<string, string, string, DateTime, byte[]>>();


    }
}

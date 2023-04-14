namespace Waddhly.DTO.Community
{
    public class CommentDTO
    {
        public int postid { get; set; }
        public string postTitle { get; set; }
        public string postuserid { get; set; }
        public string postusername { get; set; }
        public DateTime postDate { get; set; }
       
        public List<Tuple<string, string,string,DateTime>> comments { get; set; } = new List<Tuple<string, string,string,DateTime>>();


    }
}

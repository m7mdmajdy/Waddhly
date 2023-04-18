namespace Waddhly.DTO.User
{
    public class portfolioDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public DateTime Date { get; set; }
        public IFormFile File { get; set; }
        public string userId { get; set; }
        public byte[] portimage { get; set; }
    }
}

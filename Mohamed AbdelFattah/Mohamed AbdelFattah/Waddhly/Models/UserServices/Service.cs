namespace Waddhly.Models.UserServices
{
    public class Service
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public double EstimatedHours { get; set; }
        public bool Status { get; set; }
        public virtual Category category { get; set; }
        public virtual User user { get; set; }


    }
}

namespace Waddhly.Models.UserServices
{
    public class Proposal
    {
        public int ID{ get; set; }
        public double NoOfHours { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public bool? status { get; set; }
        public bool? IsDone { get; set; }
        public virtual Service service{ get; set; }
        public virtual User user{ get; set; }

    }
}

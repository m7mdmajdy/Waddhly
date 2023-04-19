namespace Waddhly.DTO.Proposal_DTO
{
    public class getpropoforuser
    {
        public int id { get; set; }
        public string userId { get; set; }
        public int serviceId { get; set; }


        public string prop_userName { get; set; }
        public string Description { get; set; }
        public double NoOfHours { get; set; }
        public double Cost { get; set; }


        public string serviceTitle { get; set; }
        public double? HourRate { get; set; }

    }
}

namespace Waddhly.DTO.Proposal_DTO
{
	public class GetProposalDTO
	{
		public int id { get; set; }
		public double NoOfHours { get; set; }
		public string Description { get; set; }
		public double Cost { get; set; }
		public string service_Title{ get; set; }
		public string service_Description { get; set; }
		public DateTime service_publishDate { get; set; }
		public string service_userName { get; set; }
		public double service_estimatedHours { get; set; }
		public bool service_Status { get; set; }
		



	}
}

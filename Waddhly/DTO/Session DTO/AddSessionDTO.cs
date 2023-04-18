namespace Waddhly.DTO.Session_DTO
{
	public class AddSessionDTO
	{
		public int ID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int proposal_id { get; set; }
	}
}

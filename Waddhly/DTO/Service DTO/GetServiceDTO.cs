namespace Waddhly.DTO
{
	public class GetServiceDTO
	{
		public int Id { get; set; }
		public string title { get; set; }
		public string Description { get; set; }
		public DateTime date { get; set; }
		public double hours { get; set; }
		public bool status { get; set; }
		public int service_category_id { get; set; }
		public string service_category_name { get; set; }
		public string user_firstname { get; set; }
		public string user_lastname { get; set; }
		public string user_title { get; set; }
        public string user_id { get; set; }

    }
}

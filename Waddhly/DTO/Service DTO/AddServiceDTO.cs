using System.ComponentModel.DataAnnotations.Schema;

namespace Waddhly.DTO
{
	public class AddServiceDTO
	{
		public int Id { get; set; }	
		public string title { get; set; }
		public string Description { get; set; }
		public double hours { get; set; }
		public string user_id { get; set; }
		public int category_id { get; set; }

	}
}

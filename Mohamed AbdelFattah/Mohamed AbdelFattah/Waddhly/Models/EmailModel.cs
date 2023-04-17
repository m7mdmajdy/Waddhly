namespace Waddhly.Models
{
	public class EmailModel
	{
		public string To { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
		public EmailModel(string to,string subj,string cont)
		{
			To = to;
			Subject = subj;
			Content = cont;
		}
	}
}

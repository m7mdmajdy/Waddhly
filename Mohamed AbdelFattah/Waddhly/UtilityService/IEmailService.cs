using Waddhly.Models;

namespace Waddhly.UtilityService
{
	public interface IEmailsender
	{

		void SendEmail(EmailModel email);
		
	}
}

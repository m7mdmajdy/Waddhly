using Waddhly.Models;

namespace Waddhly.UtilityService
{
	public interface IEmailService
	{

		void SendEmail(EmailModel email);

	}
}

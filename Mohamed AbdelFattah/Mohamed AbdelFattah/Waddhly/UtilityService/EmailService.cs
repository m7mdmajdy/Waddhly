using MailKit.Net.Smtp;
using MimeKit;
using NETCore.MailKit.Core;
using Waddhly.Models;

namespace Waddhly.UtilityService
{
	public  class EmailService:IEmailService
	{
		private readonly IConfiguration _configuration;
		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public EmailService()
		{
				
		}
		public void SendEmail(EmailModel email)
		{
			var emailMessage = new MimeMessage();
			var from = _configuration["EmailSettings:From"];
			emailMessage.From.Add(new MailboxAddress("lets program",from));
			emailMessage.To.Add(new MailboxAddress(email.To, email.To));
			emailMessage.Subject=email.Subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text=string.Format(email.Content)
			};
			using(var client=new SmtpClient())
			{    try
				{   client.Connect(_configuration["EmailSettings:SmtpServer"], 465, true);
					client.Authenticate(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
					client.Send(emailMessage);
				}
				catch (Exception ex)
				{   throw;	}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}


		}
	}


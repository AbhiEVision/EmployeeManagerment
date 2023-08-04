using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.ViewModel;
using MimeKit;
using MimeKit.Text;

namespace DemoPractical.API.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailConfigurations _configurations;

		public EmailService(EmailConfigurations configurations)
		{
			_configurations = configurations;
		}


		public Task<bool> SendMail(EmailModel model)
		{
			throw new NotImplementedException();
		}

		private MimeMessage GetMimeMessage(EmailModel model)
		{
			MimeMessage message = new MimeMessage();
			
			message.From.Add(new MailboxAddress(_configurations.UserName,_configurations.From));
			message.To.Add(new MailboxAddress(model.UserName,model.To));
			message.Body = new TextPart(TextFormat.Html) { Text = model.Body };
			message.Subject = model.Subject;
			
			return message;
		}

	}
}
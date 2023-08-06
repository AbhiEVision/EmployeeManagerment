using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractical.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		private readonly IEmailService _emailService;

		public EmailController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		[HttpPost]
		public IActionResult SendEmail(EmailModel model)
		{
			try
			{
				bool test = _emailService.SendMail(model);
				if (!test)
				{
					return BadRequest("Not Send!");
				}
				return Ok("Mail Send Successfully!");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

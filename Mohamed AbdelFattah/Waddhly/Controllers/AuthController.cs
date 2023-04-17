using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using System.Security.Cryptography;
using Waddhly.Data;
using Waddhly.DTO_Reset_Password;
using Waddhly.Helpers;
using Waddhly.Models;
using Waddhly.Models.Authentication;
using Waddhly.Services;
using Waddhly.UtilityService;
namespace Waddhly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;
		//private readonly IEmailService _emailService;

		public AuthController(IAuthService authService, ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _authService = authService;
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterAsync(registerModel);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> RegisterAsync([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.loginAsync(loginModel);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
		{
			var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
			if (user == null)
			{
				return NotFound(new
				{
					StatusCode = 404,
					Message = "Email Doesn't Exist"

				});
			}
			var tokenbytes = RandomNumberGenerator.GetBytes(64);
			var emailToken = Convert.ToBase64String(tokenbytes);
			user.RessetPasswordToken = emailToken;
			user.RessetPasswordExpiry = DateTime.Now.AddMinutes(15);
			string from = _configuration["EmailSettings:From"];

			EmailModel emailModel = new EmailModel(email, "Reset Password!", Emailbody.EmailStringBody(email, emailToken));
			_emailService.SendEmail(emailModel);

			_context.Entry(user).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return Ok(
				new
				{
					StatusCode = 200,
					Message = "Email Sent!"
				});
		}

		private static EmailModel GetEmailModel(EmailModel emailModel)
		{
			return emailModel;
		}

		[HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            var newtoken = resetPassword.EmailToken.Replace("", "+");
            var user=await _context.Users.AsNoTracking().FirstOrDefaultAsync(a=>a.Email==resetPassword.Email);
            if(user==null) { return NotFound(
                new
                {
                    StatusCode=404,
                    Message="User Doesn't Exist"
                }); 
            }
            var tokencode = user.RessetPasswordToken;
            DateTime emailTokenExpiry = user.RessetPasswordExpiry;
			if (tokencode!=resetPassword.EmailToken||emailTokenExpiry<DateTime.Now)
            {
                return BadRequest(
                    new
                    {
                        StatusCode = 400,
                        Message = "Invalid Reset Link"
                    });
            }
			PasswordHasher hasher=new PasswordHasher();

			user.PasswordHash = hasher.HashPassword(resetPassword.NewPassword);
            _context.Entry(user).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(
               new{
                   StatusCode=200,
                    Message="Password Reset Successfully"
            });
        }
    }
}

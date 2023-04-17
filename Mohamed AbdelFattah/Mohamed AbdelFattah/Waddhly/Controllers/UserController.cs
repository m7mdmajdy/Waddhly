using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waddhly.Data;
using Waddhly.DTO;

namespace Waddhly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly ApplicationDbContext _context;
		public UserController(ApplicationDbContext context)
		{
			this._context = context;

		}
		[HttpGet("{id:Guid}")]
		public IActionResult getUser([FromRoute]string id)
		{
			var user = _context.Users.FirstOrDefault(x=>x.Id==id);

			if (user == null)
			{
				return NotFound();
			}
			else
			{
				UserDTO userDTO = new UserDTO();
				userDTO.ID = user.Id;
				userDTO.FirstName = user.FirstName;
				userDTO.LastName = user.LastName;
				userDTO.Summary = user.Summary;
				userDTO.Country = user.Country;
				userDTO.HourRate = user.HourRate;
				userDTO.MoneyAccount = user.MoneyAccount;
				userDTO.Gender = user.Gender;
				return Ok(userDTO);
			}
		
		}




		//[HttpGet("id:string")]
		//public IActionResult getUser([FromHeader] string id)
		//{
		//	var user = _context.Users.FirstOrDefault(x => x.Id == id);

		//	if (user == null)
		//	{
		//		return NotFound();
		//	}
		//	else
		//	{
		//		UserDTO userDTO = new UserDTO();
		//		userDTO.ID = user.Id;
		//		userDTO.FirstName = user.FirstName;
		//		userDTO.LastName = user.LastName;
		//		userDTO.Summary = user.Summary;
		//		userDTO.Country = user.Country;
		//		userDTO.HourRate = user.HourRate;
		//		userDTO.MoneyAccount = user.MoneyAccount;
		//		userDTO.Gender = user.Gender;
		//		return Ok(userDTO);
		//	}

		//}
	}
}

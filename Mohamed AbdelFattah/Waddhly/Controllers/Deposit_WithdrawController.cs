using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.Models.UserServices;

namespace Waddhly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Deposit_WithdrawController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public Deposit_WithdrawController(ApplicationDbContext context)
		{
			this._context = context;

		}
		[HttpPut]
		public IActionResult Deposit_Withdraw(string user_id,string provider_id,double amount)
		{
			var user=_context.Users.FirstOrDefault(x => x.Id == user_id);
			var provider = _context.Users.FirstOrDefault(x => x.Id == provider_id);
			if(user == null||provider==null) { 
				return NotFound();
			}
			else
			{
				user.MoneyAccount -= amount;
				provider.MoneyAccount += amount;
				if(ModelState.IsValid)
				{
					_context.Entry(user).State = EntityState.Modified;
					_context.Entry(provider).State = EntityState.Modified;
					_context.SaveChanges();
					return Ok();
				}
				else
				{
					return BadRequest();
				}

			}


		}

	}
}

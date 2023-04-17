using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO.Proposal_DTO;
using static System.Net.Mime.MediaTypeNames;

namespace Waddhly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GetProposalbyUserIDController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public GetProposalbyUserIDController(ApplicationDbContext context)
		{
			this._context = context;

		}
		[HttpGet("{id:Guid}")]
		public IActionResult Get([FromRoute] string id)
		{
			
			var propsals = _context.Proposals.Where((p) => p.service.user.Id== id);

			List<getpropoforuser> Proposalslist = new List<getpropoforuser>();

			foreach (var item in propsals)
			{

				getpropoforuser prop = new getpropoforuser();
				prop.id = item.ID;
				prop.NoOfHours = item.NoOfHours;
				prop.Cost = item.Cost;
				prop.Description = item.Description;
				prop.prop_userName = item.user.FirstName + " " + item.user.LastName;
				prop.prop_title = item.user.Title;
				prop.HourRate = item.user.HourRate;
				Proposalslist.Add(prop);

			}
			if (Proposalslist != null)
			{
				return Ok(Proposalslist);
			}
			else
			{
				return BadRequest();
			}

		}

	}
}

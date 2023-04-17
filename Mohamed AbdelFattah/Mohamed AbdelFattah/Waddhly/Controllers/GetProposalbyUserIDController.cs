using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO.Proposal_DTO;

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
		public IActionResult getprop([FromRoute] string id)
		{
			var props = _context.Proposals.Include(s => s.service).Include(u => u.user).FirstOrDefault(p => p.user.Id==id);
			GetProposalDTO proposalDTO = new GetProposalDTO();
			proposalDTO.id = props.ID;
			proposalDTO.NoOfHours = props.NoOfHours;
			proposalDTO.Description = props.Description;
			proposalDTO.Cost = props.Cost;
			proposalDTO.service_Description = props.service.Description;
			proposalDTO.service_publishDate = props.service.PublishDate;
			proposalDTO.service_Title = props.service.Title;
			proposalDTO.service_Status = props.service.Status;
			proposalDTO.service_estimatedHours = props.service.EstimatedHours;
			proposalDTO.service_userName = props.user.FirstName + " " + props.user.LastName;

			if (proposalDTO != null)
			{
				return Ok(proposalDTO);
			}
			else
			{
				return NotFound("the Proposal you are search about not found");
			}

		}
	}
}

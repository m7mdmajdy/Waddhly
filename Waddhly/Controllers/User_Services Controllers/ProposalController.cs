using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO;
using Waddhly.DTO.Proposal_DTO;
using Waddhly.Models.UserServices;

namespace Waddhly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProposalController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public ProposalController(ApplicationDbContext context)
		{
			this._context = context;

		}
		[HttpGet]
		public IActionResult getAllPropopsals()
		{
			var test = _context.Proposals.Include(s => s.user).Include(c => c.service);
			List<GetProposalDTO> Proposals = new List<GetProposalDTO>();
			foreach (var item in test)
			{
				GetProposalDTO prop= new GetProposalDTO();
				prop.id = item.ID;
				prop.NoOfHours = item.NoOfHours;
				prop.Cost = item.Cost;
				prop.Description = item.Description;
				prop.service_Title = item.service.Title;
				prop.service_Description=item.service.Description;
				prop.service_publishDate= item.service.PublishDate;
				prop.service_userName = item.user.FirstName + "  " + item.user.LastName;
				prop.service_estimatedHours= item.service.EstimatedHours;
				prop.service_Status= item.service.Status;

				Proposals.Add(prop);
			}
			if(test!=null)
			{
				return Ok(Proposals);
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpGet("{id:int}",Name ="getpropRoute")]
		public IActionResult getPropopsalbyid(int id) 
		{ 
			var props=_context.Proposals.Include(s=>s.service).Include(u=>u.user).FirstOrDefault(p=>p.ID==id);
			GetProposalDTO proposalDTO = new GetProposalDTO();
			proposalDTO.id = props.ID;
			proposalDTO.NoOfHours=props.NoOfHours;
			proposalDTO.Description=props.Description;
			proposalDTO.Cost=props.Cost;
			proposalDTO.service_Description = props.service.Description;
			proposalDTO.service_publishDate = props.service.PublishDate;
			proposalDTO.service_Title = props.service.Title;
			proposalDTO.service_Status = props.service.Status;
			proposalDTO.service_estimatedHours = props.service.EstimatedHours;
			proposalDTO.service_userName=props.user.FirstName+" "+props.user.LastName;
			
			if (proposalDTO != null)
			{
				return Ok(proposalDTO);
			}
			else
			{
				return NotFound("the Proposal you are search about not found");
			}

		}
	
		[HttpPost]
		public IActionResult AddPropopsal(AddProposalDTO addProposal)
		{
			Proposal p=new Proposal();
			p.ID=addProposal.ID;
			p.Cost=addProposal.Cost;
			p.Description=addProposal.Description;
			p.NoOfHours = addProposal.NoOfHours;
			int id1 = addProposal.service_id;
			var _service = _context.Services.Find(id1);
			string id2 = addProposal.user_id;
			var _user = _context.Users.Find(id2);
			p.service = _service;
			p.user = _user;
			if (ModelState.IsValid == true)
			{
				_context.Proposals.Add(p);
				_context.SaveChanges();
				string url = Url.Link("getpropRoute", new { id = p.ID });
				return Created(url, p);
			}
			else
			{
				return BadRequest("The Data Tor The Propopsal You Entered Is  Not Vaild");
			}

		}
		[HttpPut]
		public IActionResult updateProposal(AddProposalDTO proposalDTO)
		{
			Proposal p = new Proposal();
			p.ID = proposalDTO.ID;
			p.Cost = proposalDTO.Cost;
			p.Description = proposalDTO.Description;
			p.NoOfHours = proposalDTO.NoOfHours;
			int id1 = proposalDTO.service_id;
			var _service = _context.Services.Find(id1);
			string id2 = proposalDTO.user_id;
			var _user = _context.Users.Find(id2);
			p.service = _service;
			p.user = _user;
			if (ModelState.IsValid)
			{
				_context.Entry(p).State = EntityState.Modified;
				_context.SaveChanges();
				return StatusCode(204, "Proposal Updated Successfully");
			}
			else
			{
				return BadRequest("nothing updated ............");
			}

		}
		
		[HttpDelete]
		public IActionResult DeleteService(int id)
		{
			Proposal prop = _context.Proposals.FirstOrDefault(e => e.ID == id);
			if ( prop!= null)
			{
				_context.Proposals.Remove(prop);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest("Proposal Not Found");
			}
		}
	}
}

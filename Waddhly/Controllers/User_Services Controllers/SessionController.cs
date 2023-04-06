using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO;
using Waddhly.DTO.Proposal_DTO;
using Waddhly.DTO.Session_DTO;
using Waddhly.Models.UserServices;

namespace Waddhly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public SessionController(ApplicationDbContext context)
		{
			this._context = context;

		}
		[HttpGet]
		public ActionResult GetAll() 
		{
			var test = _context.Sessions.Include(s => s.proposal);
			List<GetSessionDTO> sessions = new List<GetSessionDTO>();
			foreach (var item in test)
			{
				GetSessionDTO s = new GetSessionDTO();
				s.id = item.ID;
				s.StartDate=item.StartDate; 
				s.EndDate=item.EndDate;
				s.sess_prop_Desc = item.proposal.Description;
				sessions.Add(s);
			}
			if (test != null)
			{
				return Ok(sessions);
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpGet("{id:int}")]
		public IActionResult GetSessionbyid(int id)
		{
			var session=_context.Sessions.Include(c=>c.proposal).FirstOrDefault(e=>e.ID==id);
			GetSessionDTO getSession=new GetSessionDTO();
			getSession.id = session.ID;
			getSession.StartDate = session.StartDate;
			getSession.EndDate = session.EndDate;
			getSession.sess_prop_Desc = session.proposal.Description;
			if(getSession !=null)
			{
				return Ok(getSession);
			}
			else
			{
				return BadRequest();
			}
			

		}
		[HttpPost]
		public IActionResult AddSession(AddSessionDTO  sessionDTO ) 
		{  
			Session _session=new Session();
			_session.ID = sessionDTO.ID;
			_session.StartDate = sessionDTO.StartDate;
			_session.EndDate = sessionDTO.EndDate;
			int id = sessionDTO.proposal_id;
			Proposal proposal = _context.Proposals.Find(id);
			_session.proposal= proposal;
			if (ModelState.IsValid == true)
			{
				_context.Sessions.Add(_session);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest("The Data Tor The Session You Entered Is  Not Vaild");
			}

		}
		[HttpPut]
		public IActionResult updateSession(AddSessionDTO sessionDTO)
		{
			Session _session = new Session();
			_session.ID = sessionDTO.ID;
			_session.StartDate = sessionDTO.StartDate;
			_session.EndDate = sessionDTO.EndDate;
			int id = sessionDTO.proposal_id;
			Proposal proposal = _context.Proposals.Find(id);
			_session.proposal = proposal;
			if (ModelState.IsValid)
			{
				_context.Entry(_session).State = EntityState.Modified;
				_context.SaveChanges();
				return StatusCode(204, "Session Updated Successfully");
			}
			else
			{
				return BadRequest("nothing updated ............");
			}
		}

		[HttpDelete]
		public IActionResult DeleteSession(int id)
		{
			Session session = _context.Sessions.FirstOrDefault(e => e.ID == id);
			if (session != null)
			{
				_context.Sessions.Remove(session);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest("Session Not Found");
			}
		}
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO;
using Waddhly.Models.UserServices;

namespace Waddhly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public ServiceController(ApplicationDbContext context)
		{
			this._context = context;
				
		}
		[HttpGet]
		public IActionResult GetAllServices()
		{
			var test = _context.Services.Include(s => s.category).Include(c => c.user);

			List<GetServiceDTO> services = new List<GetServiceDTO>();
			foreach (var item in test)
			{
				GetServiceDTO s = new GetServiceDTO();
				s.Id = item.ID;
				s.Description= item.Description;
				s.title = item.Title;
				s.date = item.PublishDate;
				s.status = item.Status;
				s.hours = item.EstimatedHours;
				s.service_category_id = item.category.ID;
				s.service_category_name=item.category.Name;
				s.user_firstname = item.user.FirstName;
				s.user_lastname = item.user.LastName;
				s.user_title = item.user.Title;
				services.Add(s);
			}
			if(test !=null)
			{
				return Ok(services);

			}
			else
			{ return BadRequest(); }
		}
		[HttpGet("{id:int}",Name = "GetOneServiceRoute")]
		public IActionResult GetServiceByid(int id)
		{
			var service = _context.Services.Include(c => c.user).Include(s => s.category).FirstOrDefault(s => s.ID == id);
			GetServiceDTO serviceDTO = new GetServiceDTO();
			serviceDTO.Id = service.ID;
			serviceDTO.title = service.Title;
			serviceDTO.Description = service.Description;
			serviceDTO.date = service.PublishDate;
			serviceDTO.hours = service.EstimatedHours;
			serviceDTO.status = service.Status;
			serviceDTO.service_category_id = service.category.ID;
			serviceDTO.service_category_name = service.category.Name;
			serviceDTO.user_firstname = service.user.FirstName;
			serviceDTO.user_lastname = service.user.LastName;
			serviceDTO.user_title = service.user.Title;
			if (serviceDTO != null)
			{

				return Ok(serviceDTO);
			}
			else
			{
				return NotFound("the service you are search about not found");
			}


		}
		
		[HttpGet("{title:alpha}")]
		public IActionResult GetServiceByName(string title)
		{
			var service = _context.Services.Include(c => c.user).Include(s => s.category).FirstOrDefault(s => s.Title == title);

			GetServiceDTO serviceDTO = new GetServiceDTO();
			serviceDTO.Id = service.ID;
			serviceDTO.title = service.Title;
			serviceDTO.Description = service.Description;
			serviceDTO.date = service.PublishDate;
			serviceDTO.hours = service.EstimatedHours;
			serviceDTO.status = service.Status;
			serviceDTO.service_category_id = service.category.ID;
			serviceDTO.service_category_name = service.category.Name;
			serviceDTO.user_firstname = service.user.FirstName;
			serviceDTO.user_lastname = service.user.LastName;
			serviceDTO.user_title = service.user.Title;
			if (service != null)
			{

				return Ok(serviceDTO);
			}
			else
			{
				return NotFound("the service you are search about not found");
			}
		}

	[HttpPost]
		public IActionResult AddNewService(AddServiceDTO addService)
		{
			Service service=new Service();
			service.ID = addService.Id;
			service.Title = addService.title;
			service.Description=addService.Description;
			service.Status = addService.status;
			service.PublishDate = addService.date;
			service.EstimatedHours=addService.hours;
			int id1 = addService.category_id;
			var _category=_context.Categories.Find(id1);
			string id2 = addService.user_id;
			var _user=_context.Users.Find(id2);
			service.category = _category;
			service.user = _user;

			if (ModelState.IsValid == true)
			{
				_context.Services.Add(service);
				_context.SaveChanges();
				string url = Url.Link("GetOneServiceRoute", new { id = service.ID });
				return Created(url, service);
			}
			else
			{
				return BadRequest("The Data Tor The Service You Entered Is  Not Vaild");
			}
		}
		
		[HttpPut]
		public IActionResult UpdateService([FromBody] AddServiceDTO serviceDTO)
		{
			Service service = new Service();
			service.ID = serviceDTO.Id;
			service.Title = serviceDTO.title;
			service.Description = serviceDTO.Description;
			service.Status = serviceDTO.status;
			service.PublishDate = serviceDTO.date;
			service.EstimatedHours = serviceDTO.hours;
			int id1 = serviceDTO.category_id;
			var _category = _context.Categories.Find(id1);
			string id2 = serviceDTO.user_id;
			var _user = _context.Users.Find(id2);
			service.category = _category;
			service.user = _user;

		 if(ModelState.IsValid)
			{
				_context.Entry(service).State = EntityState.Modified;
				_context.SaveChanges();
				return StatusCode(204, "Service Updated Successfully");
			}
			else
			{
				return BadRequest("nothing updated ............");
			}
			
		}
		[HttpDelete]
		public IActionResult DeleteService(int id)
		{
			Service service = _context.Services.FirstOrDefault(e => e.ID == id);
			if (service != null)
			{
				_context.Services.Remove(service);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest("Service Not Found");
			}
		}
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.Models.UserServices;

namespace Waddhly.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public CategoryController(ApplicationDbContext context)
		{
			this._context = context;

		}
		[HttpGet]
		public IActionResult getAllCategories()
		{
			return Ok(_context.Categories.ToList());
		}
		[HttpGet("{id:int}",Name = "GetOneCategoryRoute")]
		public IActionResult getCategorybyid(int id)
		{
			Category category = _context.Categories.Find(id);
			if (category != null)
			{

				return Ok(category);

			}
			else
			{
				return NotFound();

			
			}
		}
		[HttpGet("{name:alpha}")]
		public IActionResult getCategorybyname(string name)
		{
			Category category = _context.Categories.FirstOrDefault(c=>c.Name==name);
			if (category != null)
			{

				return Ok(category);

			}
			else
			{
				return NotFound();


			}
		}

		[HttpPost]
		public IActionResult addCategory(Category _category)
		{

			if (ModelState.IsValid == true)
			{
				_context.Categories.Add(_category);
				_context.SaveChanges();
				string url = Url.Link("GetOneCategoryRoute", new { id = _category.ID });
				return Created(url, _category);
			}
			else
			{
				return BadRequest("The Data For The Category You Entered Is  Not Vaild");
			}

		}

		[HttpPut]
		public IActionResult updateCategory(Category _category) 
		{
			if (ModelState.IsValid)
			{
				_context.Entry(_category).State = EntityState.Modified;
				_context.SaveChanges();
				return StatusCode(204, "Category Updated Successfully");
			}
		    else
		    {
				return BadRequest("nothing updated ............");
			}
		}
		[HttpDelete]
		public IActionResult deleteCategory(int id)
		{
			Category _category = _context.Categories.FirstOrDefault(e => e.ID == id);
			if (_category != null)
			{
				_context.Categories.Remove(_category);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest("Category Not Found");
			}
		}
	}
}


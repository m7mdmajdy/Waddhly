using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waddhly.Data;
using Waddhly.Models;

namespace Waddhly.Controllers.User_Services_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            this._context = context;

        }
        [HttpGet]
        public IActionResult getAllUsers()
        {
            return Ok(_context.Users.Select(x => new {
                x.Id,x.FirstName,x.LastName,x.UserName,x.Email
            }).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult getUser(string id)
        {
            var user = _context.Users.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.UserName,
                x.Email
            }).Where(x => x.Id == id).FirstOrDefault();

            return Ok(user);
        }
    }
}

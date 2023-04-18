using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.Models;
using Waddhly.Models.Community;

namespace Waddhly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public MessageController(ApplicationDbContext _context)
        { this.context = _context; }

        [HttpGet("{userId}")]
        public IActionResult getConnectedUsers(string userId)
        {
            List<User> users = new List<User>();
            List<User> result = new List<User>();

            var rooms = context.Rooms.Include(x=>x.user1).Include(x=>x.user2).ToList();
            foreach (var room in rooms)
            {
                if (room.user1.Id == userId)
                    result.Add(room.user2);
                else if (room.user2.Id == userId)
                    result.Add(room.user1);
            }

            return Ok(result.Distinct());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Waddhly.Data;
using Waddhly.DTOs;
using Waddhly.Models;
using Waddhly.Services.Chat;

namespace Waddhly.Controllers.Connection_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public SessionController(UserManager<User> userManager, ApplicationDbContext context)
        {
            this._userManager = userManager;
            _context = context;
        }

        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserPeer(string userID)
        {
            string userPeerID = _context.Users.Find(userID).PeerId;

            return Ok(userPeerID);
        }
        [HttpPut("{userID}")]
        public async Task<IActionResult> SetUserPeer(string userID,[FromBody] string userPeerID)
        {
            var user = _context.Users.Find(userID);
            user.PeerId = userPeerID;
            _context.SaveChanges();
            return Ok(userPeerID);
        }

    }
}

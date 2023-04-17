using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Waddhly.Data;
using Waddhly.DTOs;
using Waddhly.Models;
using Waddhly.Models.Community;
using Waddhly.Services.Chat;

namespace Waddhly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chathub;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatController(IHubContext<ChatHub> chathub, UserManager<User> userManager, ApplicationDbContext context)
        {
            _chathub = chathub;
            this._userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> sendMsg([FromBody] MessageDTO roomMessage)
        {
            User Reciever = _context.Users.Find(roomMessage.RecieverId);
            User Sender = _context.Users.Find(roomMessage.SenderId);

            await _chathub.Clients.Client(Reciever.Id).SendAsync("RecieveMsg", Sender.Id, roomMessage.Content);
            _context.SaveChanges();
            return Ok(roomMessage);
        }
    }
}

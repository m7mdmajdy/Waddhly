using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Waddhly.Models.Community;
using Waddhly.Models;
using Waddhly.Data;

namespace Waddhly.Services.Chat
{
    public class ChatHub :Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string recieverConnectionId, string message)
        {

            User Reciever = _context.Users.Find(user);
            User Sender = _context.Users.Find(recieverConnectionId);
            RoomMessage roomMessage = new RoomMessage { Content = message, Reciever = Reciever, Sender = Sender };
            _context.RoomMessages.Add(roomMessage);
             _context.SaveChanges();

            await Clients.Client(recieverConnectionId).SendAsync("RecieveMsg", user, message);
        }
        public string GetConnectionId() => Context.ConnectionId;
    }
}

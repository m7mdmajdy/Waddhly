using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Waddhly.Models.Community;
using Waddhly.Models;
using Waddhly.Data;
using Waddhly.DTOs;

namespace Waddhly.Services.Chat
{
    public class ChatHub :Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task sendMsg(string userReciverConnId,string userId2, string userId, string message)
        {
            User userSender = _context.Users.FirstOrDefault(x => x.Id == userId2);
            User userReciever = _context.Users.FirstOrDefault(x => x.Id == userId);
            RoomMessage roomMessage = new RoomMessage { Content = message, Sender = userSender, Date = DateTime.Now, Reciever = userReciever };
            _context.RoomMessages.Add(roomMessage);
            _context.SaveChanges();
            string x = Context.ConnectionId;
            List<MessageDTO> messageDTOs = _context.RoomMessages.Select(x => new MessageDTO
            {
                Date = x.Date,
                SenderId = x.Sender.Id,
                senderName = x.Sender.UserName,
                RecieverId = x.Reciever.Id,
                RecieverName = x.Reciever.UserName,
                Content = x.Content,
            }).Where(x => (x.SenderId == userSender.Id && x.RecieverId == userReciever.Id) ||
            (x.SenderId == userReciever.Id && x.RecieverId == userSender.Id)).OrderBy(x => x.Date).ToList();

            if (userReciverConnId != null)
            {
                await Clients.Client(userReciverConnId).SendAsync("sendMsgResponse", Context.ConnectionId, userSender.Id, messageDTOs);
            }
        }
        public async Task updateConnectionId(string userId)
        {
            User user = _context.Users.Find(userId);
            user.ConnectionId = Context.ConnectionId;
            _context.SaveChanges();
        }
        public async Task getMessages(string user, string user2)
        {
            string x = Context.ConnectionId;
            List<MessageDTO> messageDTOs = _context.RoomMessages.Select(x=> new MessageDTO
            {
                Date=x.Date,
                SenderId=x.Sender.Id,
                senderName=x.Sender.UserName,
                RecieverId=x.Reciever.Id,
                RecieverName=x.Reciever.UserName,
                Content=x.Content,
            }).Where(x => (x.SenderId==user && x.RecieverId==user2) || (x.SenderId==user2 && x.RecieverId==user)).ToList();

          

            await Clients.Client(user).SendAsync("sendMessagesResponse", Context.ConnectionId, messageDTOs);
        }
        public async Task askServer(string someTextFromClient)
        {
            string tempString;
            if (someTextFromClient == "hey")
            {
                tempString = "messge he";
            }
            else
            {
                tempString = "message not hey";
            }
            await Clients.Client(this.Context.ConnectionId).SendAsync("askServerResponse", tempString);
        }
        public string GetConnectionId() => Context.ConnectionId;
        public string GetConnectionIdByUserId(string userId)
        {
            return _context.Users.Find(userId).ConnectionId;
        }
    }
}

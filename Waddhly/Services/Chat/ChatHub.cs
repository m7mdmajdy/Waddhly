using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Waddhly.Models.Community;
using Waddhly.Models;
using Waddhly.Data;
using Waddhly.DTOs;
using Waddhly.Models.UserServices;

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

            var oldMessagesValidation = _context.Rooms
                .Where(x => (x.user1.Id == userId && x.user2.Id == userId2) || (x.user1.Id == userId2 && x.user2.Id == userId)).ToList();
            if (oldMessagesValidation.Count == 0)
            {
                Rooms room = new Rooms { user1=userSender, user2=userReciever };
                _context.Rooms.Add(room);
                _context.SaveChanges();
            }

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
        public async Task updatePeerId(string userId, string peerID)
        {
            var user = _context.Users.Find(userId);
            user.PeerId = peerID;
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
        public string GetPeerIdByUserId(string userName)
        {
            return _context.Users.FirstOrDefault(x=>x.UserName== userName).PeerId;
        }
        public string SendUserMessage(string userSender, string userReciever, string message)
        {
            User Reciver = _context.Users.Find(userReciever);
            User Sende = _context.Users.Find(userSender);
            RoomMessage roomMessage = new RoomMessage
            {
                Content = message,
                Reciever = Sende,
                Sender = Reciver,
                Date = DateTime.Now,
            };
            var oldMessagesValidation = _context.Rooms
                .Where(x => (x.user1.Id == userSender && x.user2.Id == userReciever) || (x.user1.Id == userReciever && x.user2.Id == userSender)).ToList();
            if (oldMessagesValidation.Count == 0)
            {
                Rooms room = new Rooms
                {
                    user1 = Sende,
                    user2 = Reciver
                };
                _context.Rooms.Add(room);
            }


            _context.RoomMessages.Add(roomMessage);
            _context.SaveChanges();
            return "asdf";
        }

        public bool isValidProposal(string student, string teacher)
        {
            var proposal = _context.Proposals
                .Include(x => x.service)
                .ThenInclude(x => x.user)
                .Where(x=>((x.user.UserName == teacher && x.service.user.UserName == student) ||
                (x.user.UserName == student && x.service.user.UserName == teacher))
                && x.status == true && x.IsDone==false)
                .OrderByDescending(x=>x.ID).FirstOrDefault();
            if (proposal != null)
            {
                return true;
            }
            return false;
        }
        public bool StartSession(string student, string teacher)
        {

            var proposal = _context.Proposals
                .Include(x => x.service)
                .ThenInclude(x => x.user)
                .Where(x => ((x.user.UserName == teacher && x.service.user.UserName == student) ||
                (x.user.UserName == student && x.service.user.UserName == teacher))
                && x.status == true)
                .OrderByDescending(x => x.ID).FirstOrDefault();
            if (proposal != null)
            {
                Session session = new Session
                {
                    proposal = proposal,
                    StartDate= DateTime.Now,
                };
                _context.Sessions.Add(session);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool EndSession(string student, string teacher)
        {

            var session = _context.Sessions.Include(x => x.proposal)
                 .ThenInclude(x => x.service)
                 .ThenInclude(x => x.user)

                 .Where(x => ((x.proposal.user.UserName == teacher && x.proposal.service.user.UserName == student) ||
                 (x.proposal.user.UserName == student && x.proposal.service.user.UserName == teacher))
                 && x.proposal.status == true && x.proposal.IsDone==false)
                 .OrderByDescending(x => x.ID).FirstOrDefault();

            if (session != null)
            {
                session.EndDate = DateTime.Now;
                _context.SaveChanges();
                var proposal = _context.Proposals
                .Include(x => x.service)
                .ThenInclude(x => x.user)
                .Where(x => ((x.user.UserName == teacher && x.service.user.UserName == student) ||
                (x.user.UserName == student && x.service.user.UserName == teacher))
                && x.status == true)
                .OrderByDescending(x => x.ID).FirstOrDefault();

                var priceOfMinute = (proposal.Cost / proposal.NoOfHours)/60;

                TimeSpan difference = session.EndDate.Value - session.StartDate.Value;
                double totalMinutes = difference.TotalMinutes;
                session.exactCost= totalMinutes* priceOfMinute;
                session.isPaid = false;
                proposal.IsDone=true;
                _context.SaveChanges();

                return true;
            }
            return false;
        }
        public void changeStatus(int id)
        {
            var prop = _context.Proposals.FirstOrDefault(x=>x.ID==id);
            prop.status = true;
            _context.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO.Community;

namespace Waddhly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ApplicationDbContext context;

        public CommentController(ApplicationDbContext _context)
        {
            this.context = _context;

        }
        [HttpGet]
        public ActionResult getAllcoment()
        {

            var post = context.Posts.Include(c => c.Comments).Include(u => u.user).ToList();
            //var co = context.Comments.Include("post").Include(c=>c.user).ToList();
            List<CommentDTO> list = new List<CommentDTO>();
            if (post != null)
            {
                foreach (var pst in post)
                {
                    CommentDTO comtDto = new CommentDTO();

                    comtDto.postid = pst.ID;
                    comtDto.postTitle = pst.Title;
                    comtDto.postuserid = pst.user.Id;
                    comtDto.postusername = $"{pst.user.FirstName} {pst.user.LastName}";
                    foreach (var c in pst.Comments)
                    {
                        var nam= $"{c.user.FirstName} {c.user.LastName}";
                        comtDto.comments.Add(new Tuple<string, string,string ,DateTime>(c.Description, c.user.Id,nam,c.Date));
                    }
                    list.Add(comtDto);

                }

                return Ok(list);
            }
            return BadRequest("no post found");
        }
        [HttpGet("{id}")]
        public ActionResult getpostcomment(int id)
        {
            var post = context.Posts.Include(c => c.Comments).Include(u => u.user).FirstOrDefault(u=>u.ID==id);
            if (post != null)
            {
                CommentDTO comtDto = new CommentDTO();

                comtDto.postid = post.ID;
                comtDto.postTitle = post.Title;
                comtDto.postuserid = post.user.Id;
                comtDto.postusername = $"{post.user.FirstName} {post.user.LastName}";
                foreach (var c in post.Comments)
                {
                    var nam = $"{c.user.FirstName} {c.user.LastName}";

                    comtDto.comments.Add(new Tuple<string, string, string, DateTime>(c.Description, c.user.Id,nam, c.Date));
                }
                return Ok(comtDto);
            }
            else { return BadRequest("post not found"); }
        }
    }
}

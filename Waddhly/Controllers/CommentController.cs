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
                    foreach (var c in pst.Comments)
                    {
                        comtDto.comments.Add(new Tuple<string, string>(c.Description, c.user.Id));
                    }
                    list.Add(comtDto);

                }

                return Ok(list);
            }
            return BadRequest("no post found");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO.Community;
using Waddhly.Models.Community;

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

            var post = context.Posts.Include(c => c.Comments).ThenInclude(c => c.user).Include(u => u.user).ToList();
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
                    comtDto.postDescription = pst.Description;
                    comtDto.postDate = pst.Date;
                    comtDto.userimage = pst.user.image;
                    comtDto.postusername = $"{pst.user.FirstName} {pst.user.LastName}";
                    foreach (var c in pst.Comments)
                    {
                        var nam = $"{c.user.FirstName} {c.user.LastName}";
                        comtDto.comments.Add(new Tuple<string, string, string, DateTime, byte[]>(c.Description, c.user.Id, nam, c.Date, c.user.image));
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
            var post = context.Posts.Include(c => c.Comments).ThenInclude(s => s.user).Include(u => u.user).FirstOrDefault(u => u.ID == id);
            if (post != null)
            {
                CommentDTO comtDto = new CommentDTO();

                comtDto.postid = post.ID;
                comtDto.postTitle = post.Title;
                comtDto.postuserid = post.user.Id;
                comtDto.postDescription = post.Description;
                comtDto.postDate = post.Date;
                comtDto.userimage = post.user.image;
                comtDto.postusername = $"{post.user.FirstName} {post.user.LastName}";
                foreach (var c in post.Comments)
                {
                    var nam = $"{c.user.FirstName} {c.user.LastName}";

                    comtDto.comments.Add(new Tuple<string, string, string, DateTime, byte[]>(c.Description, c.user.Id, nam, c.Date, c.user.image));
                }
                return Ok(comtDto);
            }
            else { return BadRequest("post not found"); }
        }
        [HttpPost]
        public ActionResult AddComment(int id, AddcommentDto commentdto)
        {
            var post = context.Posts.FirstOrDefault(p => p.ID == id);
            if (post != null)
            {
                Comment c = new Comment();
                c.Description = commentdto.commentdescription;
                c.Date = DateTime.Now;
                c.postid = post.ID;
                c.userid = commentdto.commentUserId;
                context.Comments.Add(c);
                context.SaveChanges();
                return NoContent();


            }
            else return BadRequest("Post Not Found To Add Comment");

        }
        [HttpDelete]
        public ActionResult DeleteComment(int postid, int commentid)
        {
            var comment = context.Comments.Include(p => p.post).FirstOrDefault(c => c.postid == postid && c.ID == commentid);
            if (comment != null)
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
                return NoContent();
            }
            return BadRequest("comment not found to delete");

        }
    }
}

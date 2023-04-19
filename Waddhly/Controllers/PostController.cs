using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waddhly.Data;
using Waddhly.DTO.Community;
using Waddhly.Models.Community;

namespace Waddhly.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public PostController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpPost]
        public ActionResult addPost(PostDto postdto)
        {
            Post post = new Post();
            post.Title = postdto.Title;
            post.userid = postdto.userid;
            post.Description = postdto.Description;
            post.Date = DateTime.Now;
            context.Posts.Add(post);
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete]
        public ActionResult deletePost(int id)
        {
            var post = context.Posts.FirstOrDefault(p => p.ID == id);
            var comments = context.Comments.Where(c => c.postid == post.ID).ToList();
            if (post != null)
            {
                context.Comments.RemoveRange(comments);
                context.Posts.Remove(post);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest("post not found");
        }
        //1- post class
        //2-comment class
        //3-add postdto and addcommentdto
        //4- commentController
        //5-add postcontroller


    }
}

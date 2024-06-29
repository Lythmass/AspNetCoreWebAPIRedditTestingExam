using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Models;

namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreateComment(CommentDto commentDto)
        {
            var comment = new Comment
            {
                Content = commentDto.Content,
                PostId = commentDto.PostId,
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = comment.Id }, comment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> Get(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetCommentsForPost(int postId) {
           return await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

    }
}

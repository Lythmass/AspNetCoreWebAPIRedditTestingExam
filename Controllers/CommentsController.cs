using Microsoft.AspNetCore.Mvc;
using Reddit.Dtos;
using Reddit.Mapper;
using Reddit.Models;

namespace Reddit.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class CommentsController : ControllerBase
        {
            private readonly ApplcationDBContext _context;

            public CommentsController(ApplcationDBContext context)
            {
                _context = context;
            }

        [HttpPost]
        public async Task<ActionResult<Post>> CreateComment(CommentDto commentDto)
        {
            var post = await _context.Posts.FindAsync(commentDto.PostId);

            var comment = new Comment
            {
                Content = commentDto.Content,
                PostId = commentDto.PostId,
                Post = post
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = comment.Id }, comment);
        }
    }
}

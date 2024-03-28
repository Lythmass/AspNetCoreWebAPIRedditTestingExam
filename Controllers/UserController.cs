using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Mapper;
using Reddit.Models;

namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateUserDto createAuthorDto)
        {
            var author = new User
            {
                Name = createAuthorDto.Name
            };

            await _context.Users.AddAsync(author);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAuthors()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpPost("JoinCommunity")]
        public async Task<IActionResult> JoinCommunity(int userId,int communityId)
        {
            var community = await _context.Communities.FindAsync(communityId);

            if (community == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.SubscribedCommunities.Add(community);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
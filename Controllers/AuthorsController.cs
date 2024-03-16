using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Mapper;
using Reddit.Models;

namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplcationDBContext _context;

        public AuthorsController(ApplcationDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            var author = new Author
            {
                Name = createAuthorDto.Name
            };

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}
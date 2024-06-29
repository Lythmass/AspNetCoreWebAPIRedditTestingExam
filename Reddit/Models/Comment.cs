using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reddit.Models
{
    [Owned]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PostId { get; set; }
        public Post? Post { get; set; }

    }
}
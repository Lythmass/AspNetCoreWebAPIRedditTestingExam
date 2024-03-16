using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reddit.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int? AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual User? Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public int  Upvotes { get; set; }
        public int  Downvotes{ get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}

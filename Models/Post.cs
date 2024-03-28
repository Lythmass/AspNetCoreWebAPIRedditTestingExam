using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Reddit.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? AuthorId { get; set; }
        public virtual User? Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public int  Upvotes { get; set; }
        public int  Downvotes{ get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        public virtual Community Community { get; set; }
        public virtual int CommunityId { get; set; }

    }
}

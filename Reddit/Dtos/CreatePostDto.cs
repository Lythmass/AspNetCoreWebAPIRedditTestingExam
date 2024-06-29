using System.ComponentModel.DataAnnotations;

namespace Reddit.Dtos
{
    public class CreatePostDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public int CommunityId { get; set; }
    }
}

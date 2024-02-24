using System.ComponentModel.DataAnnotations;

namespace Reddit.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
    }
}
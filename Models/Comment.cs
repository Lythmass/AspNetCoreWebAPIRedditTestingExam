using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Reddit.Models
{
    [Owned]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

    }
}
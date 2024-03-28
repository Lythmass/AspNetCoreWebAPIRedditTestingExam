using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reddit.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        [InverseProperty("Subscribers")]
        public virtual ICollection<Community>? SubscribedCommunities { get; set; } = new List<Community>();

        [InverseProperty("Owner")]
        public virtual ICollection<Community>? OwnedCommunities { get; set; } = new List<Community>();

    }
}
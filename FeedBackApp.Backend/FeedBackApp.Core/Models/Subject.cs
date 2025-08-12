
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Backend.Core.Models
{
    public class Subject
    {
        [Key]
        public Guid Guid { get; set; } // public string Id { get; set; } = Guid.NewGuid().ToString(); Maybe this will be needed for cosmosDB

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [Range(5, 12)]
        public short Grade { get; set; }
        
        public ICollection<Person> Teachers { get; set; } = new List<Person>();
        
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

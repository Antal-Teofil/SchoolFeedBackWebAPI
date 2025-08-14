
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Backend.Core.Models
{
    public class Subject
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;
        
    }
}

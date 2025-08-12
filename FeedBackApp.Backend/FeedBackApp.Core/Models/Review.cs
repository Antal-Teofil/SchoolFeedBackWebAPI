
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Backend.Core.Models
{
    public class Review
    {
        [Key]
        public string Id { get; set; } = String.Empty; // hash based on student teacher and subject triplet

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [Range(0, 5)]
        public short? Score { get; set; }

        [MaxLength(250)]
        public string? Comment { get; set; } = String.Empty;

        [Required]
        public Guid SubjectId { get; set; } = new();
        
        public Subject Subject { get; set; } = new(); // navigation for EF

        [Required]
        public Guid TeacherId { get; set; } = new();
        
        public Person Teacher { get; set; } = new(); // navigation for EF

    }
}

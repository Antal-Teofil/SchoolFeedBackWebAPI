
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Backend.Core.Models
{
    public class Review
    {
        [Key]
        public byte[] Id { get; set; } = [];
        public string Name { get; set; } = String.Empty;
        public short? Score { get; set; }
        public string? Comment { get; set; } = String.Empty;
        public Guid SubjectId { get; set; } = new();
        public Subject Subject { get; set; } = new(); // navigation for EF
        public Guid TeacherId { get; set; } = new();
        public Person Teacher { get; set; } = new(); // navigation for EF

    }
}

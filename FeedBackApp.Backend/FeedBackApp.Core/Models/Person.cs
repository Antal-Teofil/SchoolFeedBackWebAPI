
using FeedBackApp.Backend.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Backend.Core.Models
{
    public class Person
    {
        [Key]
        public Guid Guid { get; set; } // public string Id { get; set; } = Guid.NewGuid().ToString(); Maybe this will be needed for cosmosDB

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public byte[] Password { get; set; } = []; // changable to string

        [Required]
        public Role Role { get; set; } = Role.Student;

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    }
}

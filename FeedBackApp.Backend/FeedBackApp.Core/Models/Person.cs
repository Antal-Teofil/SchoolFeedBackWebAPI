
using FeedBackApp.Backend.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Backend.Core.Models
{
    public class Person
    {
        [Key]
        public Guid Guid { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public byte[] Password { get; set; } = [];

        public Role Role { get; set; } = Role.Student;

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    }
}

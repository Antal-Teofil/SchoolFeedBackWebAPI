
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Backend.Core.Models
{
    public class Subject
    {
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; } = String.Empty;
        public short Grade { get; set; }
        public ICollection<Person> Teachers { get; set; } = new List<Person>();
    }
}

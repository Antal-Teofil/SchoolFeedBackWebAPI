
using FeedBackApp.Backend.Core.Models;

namespace FeedBackApp.Backend.Core.Interfaces
{
    public interface IPersonsRepository
    {
        public Person? GetPersonById(Guid id);

        public Person? GetPersonByEmail(string email);

        public ICollection<Subject> GetSubjects(Guid id);

        public bool AddPerson(Person person);

        public bool DeletePerson(Guid id);

        public bool UpdatePerson(Person person);
    }
}

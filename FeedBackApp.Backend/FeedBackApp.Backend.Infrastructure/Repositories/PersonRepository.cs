
using FeedBackApp.Backend.Core.Models;
using FeedBackApp.Core.Interfaces;

namespace FeedBackApp.Backend.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public bool AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerson(Guid id)
        {
            throw new NotImplementedException();
        }

        public Person? GetPersonByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Person? GetPersonById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Subject> GetSubjects(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}

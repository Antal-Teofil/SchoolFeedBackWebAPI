
using FeedBackApp.Backend.Core.Models;

namespace FeedBackApp.Backend.Core.Interfaces
{
    public interface ISubjectsRepository
    {
        public Subject? GetSubjectById(Guid id);

        public ICollection<Person> GetTeachersBySubject(Subject subject);

        public ICollection<Person> GetTeachersBySubjectId(Guid id);

        public ICollection<Subject> GetSubjectsByGrade(short grade);

        public bool AddSubject(Subject subject);

        public bool DeleteSubject(Guid id);

        public bool UpdateSubject(Subject subject);
    }
}

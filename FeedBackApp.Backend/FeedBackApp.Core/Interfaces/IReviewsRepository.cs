using FeedBackApp.Backend.Core.Models;

namespace FeedBackApp.Core.Interfaces
{
    public interface IReviewsRepository
    {
        public ICollection<Review> GetAllReviews();

        public Review GetReview(byte[] id);

        public Review GetTeacherForReview(Guid teacherId, Guid subjectId);

        public ICollection<Review> GetReviewsBySubject(byte[] subjectId);

        public bool AddReview(Review review);

        public bool DeleteReview(byte[] id);

        public bool UpdateReview(Review review);
    }
}


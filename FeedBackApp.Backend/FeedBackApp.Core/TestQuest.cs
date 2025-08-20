using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils;
using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityModels;
using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs;
namespace FeedBackApp.Core
{
    public class Test
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Masnapos vagyok mint a szar...");

            var teachersFromDb = new[]
            {
            new Teacher("t1@school.com", "János", "Kiss", "Matematika"),
            new Teacher("t2@school.com", "Anna", "Nagy", "Fizika")
            };

            var adminsFromDb = new[]
            {
                new Administrator("a1@school.com", "Péter", "Kovács")
            };

            // nagyon sexy
            var teacherContainer = new RecipientContainer<Teacher>(teachersFromDb);
            Console.WriteLine(teacherContainer.GetType());
            var teacherReviews = ReviewCompiler.CompileReviews<TeacherReview<PortableDocumentFormat>>(teacherContainer);
            var teacherrev = ReviewCompiler.CompileReviews<TeacherReview<MicrosoftExcelFormat>>(teacherContainer);
            Console.WriteLine(teacherReviews.GetType());

            var adminContainer = new RecipientContainer<Administrator>(adminsFromDb);
            Console.WriteLine(adminContainer.GetType());
            var adminReviews = ReviewCompiler.CompileReviews<AdministratorReview<MicrosoftExcelFormat>>(adminContainer);
            Console.WriteLine(adminReviews.GetType());
        }
    }
}

namespace FeedBackApp.Backend.Infrastructure.Middleware.Utils
{

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class RequireAdminAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class RequireStudentAttribute : Attribute { }

}

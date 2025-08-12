
using FeedBackApp.Backend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedBackApp.Backend.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many: Person <-> Subject
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Subjects)
                .WithMany(s => s.Teachers)
                .UsingEntity(j => j.ToTable("PersonSubjects"));

            // One-to-many: Subject -> Reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Subject)
                .WithMany(s => s.Reviews)
                .HasForeignKey(r => r.SubjectId)
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: Teacher(Person) -> Reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Teacher)
                .WithMany()
                .HasForeignKey(r => r.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}

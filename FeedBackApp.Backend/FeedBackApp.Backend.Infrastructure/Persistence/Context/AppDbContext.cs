
using FeedBackApp.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace FeedBackApp.Backend.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<Metadata> Metadatas { get; set; } = null!;
        public DbSet<Questionnaire> Questionnaires { get; set; } = null!;
        public DbSet<ReviewIndex> ReviewIndices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseCosmos(
            accountEndpoint: "https://localhost:8081",
            accountKey: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            databaseName: "SchoolDatabase"
        );


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultContainer("MainContainer");

            modelBuilder.Entity<Metadata>().ToContainer("MainContainer")
                .HasPartitionKey(m => m.Id)
                .Property(m => m.Id).ToJsonProperty("id");

            modelBuilder.Entity<ReviewIndex>().ToContainer("MainContainer")
                .HasPartitionKey(r => r.Id)
                .Property(r => r.Id).ToJsonProperty("id");

            modelBuilder.Entity<Questionnaire>().ToContainer("MainContainer")
                .HasPartitionKey(q => q.Id)
                .Property(q => q.Id).ToJsonProperty("id");
        }

    }
}

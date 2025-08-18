
using FeedBackApp.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FeedBackApp.Backend.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<Metadata> Metadatas { get; set; } = null!;
        public DbSet<Questionnaire> Questionnaires { get; set; } = null!;

        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

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

            modelBuilder.Entity<Metadata>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.HasPartitionKey(m => m.Id);
            });

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.HasPartitionKey(q => q.PartitionKey);
            });

            modelBuilder.Entity<Metadata>().HasDiscriminator<string>("docType").HasValue<Metadata>("Metadata");
            modelBuilder.Entity<Questionnaire>().HasDiscriminator<string>("docType").HasValue<Questionnaire>("Questionnaire");

        }

    }
}

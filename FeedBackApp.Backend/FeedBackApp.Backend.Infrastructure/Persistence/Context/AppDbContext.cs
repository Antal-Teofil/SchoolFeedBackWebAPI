
using FeedBackApp.Core.Model;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace FeedBackApp.Backend.Infrastructure.Persistence
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        //    => optionsBuilder.UseCosmos(
        //    accountEndpoint: "https://localhost:8081",
        //    accountKey: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
        //    databaseName: "SchoolDatabase"
        //);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultContainer("mainContainer");

            modelBuilder.Entity<SurveyMetadata>()
                .ToContainer("mainContainer")
                .HasPartitionKey(m => m.Id)
                .HasKey(m => m.Id);

            modelBuilder.Entity<Questionnaire>()
                .ToContainer("mainContainer")
                .HasPartitionKey(q => q.PartitionKey)
                .HasKey(q => q.Id); // maybe partitionkey is enough

            modelBuilder.Entity<Questionnaire>()
                .OwnsMany(q => q.QuestionnaireResults);

            // add discriminator
               
        }

    }
}

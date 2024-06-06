using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Models.Entities;

namespace MultiLanguageExamManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LocalizationResource> LocalizationResources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Your code here
            // One contry can have multiple languages, once you delete a country, all languages related
            // to it should be deleted, but not vice versa

            // One language can have multiple localization resources, once you delete a language, all localization resources related
            // to it should be deleted, but not vice versa

            // Localization resource table should have a unique index,
            // meaning that there can not be two records that have the same namespace and key
        }

    }
}

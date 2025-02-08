using Microsoft.EntityFrameworkCore;
using Task_Management_API.Models;

namespace Task_Management_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<TaskTemplate> Tasks { get; set; }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql("Host=db.wnnnwalvgahsuvdcbtoy.supabase.co;Database=postgres;User ID=postgres;Password=welcome234;Port=5432;");
    //    }

    
        //seeding the database
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskTemplate>().HasData
                (
                new TaskTemplate
                {
                    Id = 1,
                    Name = "Test",
                    IsCompleted = true
                }
                 );
                        
        }
    }
}
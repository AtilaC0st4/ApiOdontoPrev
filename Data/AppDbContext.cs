using Microsoft.EntityFrameworkCore;
using OdontoPrev.Models;

namespace OdontoPrev.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BrushingRecord> BrushingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
     .HasMany(u => u.BrushingRecords)
     .WithOne(br => br.User!)
     .HasForeignKey(br => br.UserId)
     .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
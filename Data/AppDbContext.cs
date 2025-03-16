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
    }
}
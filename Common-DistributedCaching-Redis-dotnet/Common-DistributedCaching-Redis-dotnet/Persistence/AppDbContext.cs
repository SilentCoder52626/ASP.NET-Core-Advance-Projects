using Common_InMemoryCaching_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Common_InMemoryCaching_dotnet.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
        }
    }
}

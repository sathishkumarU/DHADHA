using Microsoft.EntityFrameworkCore;
using FirstControllerProject.Models;

namespace FirstControllerProject.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<UserMaster> UserMaster => Set<UserMaster>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Only if this table has no primary key
            modelBuilder.Entity<UserMaster>()
                .ToTable("UserMaster", "Common");
        }
}

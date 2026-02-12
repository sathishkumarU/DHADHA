using Microsoft.EntityFrameworkCore;
using FirstControllerProject.Models;

namespace FirstControllerProject.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<DHADHMembersMaster> DHADHMembersMaster => Set<DHADHMembersMaster>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Only if this table has no primary key
            modelBuilder.Entity<DHADHMembersMaster>()
                .ToTable("DHADHMembersMaster", "DHADHA");
        }
}

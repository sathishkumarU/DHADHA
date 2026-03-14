using Microsoft.EntityFrameworkCore;
using FirstControllerProject.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FirstControllerProject.Models.BaseClass;

namespace FirstControllerProject.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    public bool AuditRequired =true;
    public DbSet<UserMaster> UserMaster => Set<UserMaster>();
    public DbSet<veUserMaster> veUserMaster => Set<veUserMaster>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Only if this table has no primary key
            modelBuilder.Entity<UserMaster>()
                .ToTable("UserMaster", "Common");
            modelBuilder.Entity<veUserMaster>().ToView("veUserMaster","Common").HasNoKey();
        }
    public override int SaveChanges()
    {
        if(AuditRequired)
        {
            DateTime CurrentDate =  DateTime.UtcNow;
            var Entity = ChangeTracker.Entries<AuditBase>();
            foreach(var entry in Entity)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = CurrentDate;
                    entry.Entity.CreatedBy = "Admin";
                    entry.Entity.DHStatus = "A";
                    entry.Entity.UniqueId = Guid.NewGuid();
                    entry.Property("LastUpdate").IsModified = false;
                    entry.Property("LastUpdateBy").IsModified = false;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.Entity.LastUpdate = CurrentDate;
                    entry.Entity.LastUpdateBy = "Admin";
                    entry.Property("CreatedDate").IsModified = false;
                    entry.Property("CreatedBy").IsModified = false;

                }
                
            }
        }
        return base.SaveChanges();
    }
}

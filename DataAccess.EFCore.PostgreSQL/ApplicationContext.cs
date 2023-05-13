using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.PostgreSQL;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Example> Examples => Set<Example>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = String.IsNullOrEmpty(entry.Entity.CreatedBy) ? "System" : entry.Entity.CreatedBy;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.Now;
                    entry.Entity.UpdatedBy = String.IsNullOrEmpty(entry.Entity.UpdatedBy) ? "System" : entry.Entity.UpdatedBy;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}


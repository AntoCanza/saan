using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SaAn.Application.Interfaces;
using SaAn.Domain;
using SaAn.Domain.Interfaces;

namespace SaAn.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
{
    public const string DefaultSchema = "dbo";

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DateTime dateTime = DateTime.UtcNow;

        foreach (EntityEntry<AAuditable> entry in ChangeTracker.Entries<AAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreationDate = DateOnly.FromDateTime(dateTime);
                    entry.Entity.CreationTime = TimeOnly.FromDateTime(dateTime);
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateOnly.FromDateTime(dateTime);
                    entry.Entity.UpdatedTime = TimeOnly.FromDateTime(dateTime);
                    break;
            }
        }

        foreach (EntityEntry<ISearchableLowerCase> entry in ChangeTracker.Entries<ISearchableLowerCase>())
        {
            entry.Entity.SetLowerCaseField();
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
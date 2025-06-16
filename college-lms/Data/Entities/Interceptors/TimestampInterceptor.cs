using college_lms.Data.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class TimestampsInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = eventData
            .Context.ChangeTracker.Entries()
            .Where(e =>
                e.Entity is IWithTimestamps
                && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        foreach (var entry in entries)
        {
            var entity = (IWithTimestamps)entry.Entity;

            var utcNow = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = utcNow;
            }

            entity.UpdatedAt = utcNow;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}

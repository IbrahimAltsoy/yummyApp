using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using yummyApp.Application.Abstract.Common;
using yummyApp.Domain.Common;

namespace yummyApp.Persistance.Interceptors
{
    public class AuditableEntityInterceptor: SaveChangesInterceptor
    {
        readonly IUser _currentUser;
        readonly TimeProvider _dateTime;

        public AuditableEntityInterceptor(IUser currentUser, TimeProvider dateTime)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {

            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext context)
        {
            if (context == null) return;

            var entries = context.ChangeTracker.Entries<IAuditableEntity<Guid>>();

            foreach (var entry in entries)
            {
                
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(o => o.CreatedAt).CurrentValue = _dateTime.GetUtcNow().DateTime;
                    entry.Property(o => o.CreatedBy).CurrentValue = _currentUser.Id;
                }
                else if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Property(o => o.LastModifiedAt).CurrentValue = _dateTime.GetUtcNow().DateTime;
                    entry.Property(o => o.LastModifiedBy).CurrentValue = _currentUser.Id;
                }
                else if (entry.State == EntityState.Deleted || (
                    entry.State == EntityState.Modified &&
                    entry.Property(o => o.DeletedAt).CurrentValue != null &&
                    entry.Property(o => o.DeletedAt).OriginalValue == null))
                {
                    entry.Property(o => o.DeletedAt).CurrentValue = _dateTime.GetUtcNow().DateTime;
                    entry.Property(o => o.DeletedBy).CurrentValue = _currentUser.Id;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }

    public static class EntityExtensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}


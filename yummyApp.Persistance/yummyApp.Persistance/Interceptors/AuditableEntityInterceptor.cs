using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using yummyApp.Application.Abstract.Common;
using yummyApp.Domain.Common;

namespace yummyApp.Persistance.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        readonly IUser _currentUser;
        readonly TimeProvider _dateTime;

        public AuditableEntityInterceptor(IUser currentUser, TimeProvider dateTime)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context == null)
                throw new ArgumentNullException(nameof(eventData.Context));

            UpdateEntities(eventData.Context).GetAwaiter().GetResult();

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context == null)
                throw new ArgumentNullException(nameof(eventData.Context));

            await UpdateEntities(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task UpdateEntities(DbContext context)
        {
            var entries = context.ChangeTracker.Entries<IAuditableEntity<Guid>>();
            var userId = _currentUser.Id;
            var utcNow = _dateTime.GetUtcNow().DateTime;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(IAuditableEntity<Guid>.CreatedAt)).CurrentValue = utcNow;
                    entry.Property(nameof(IAuditableEntity<Guid>.CreatedBy)).CurrentValue = userId;
                }

                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    // Update LastModified fields only if not soft deleting
                    if (!IsSoftDeleting(entry))
                    {
                        entry.Property(nameof(IAuditableEntity<Guid>.LastModifiedAt)).CurrentValue = utcNow;
                        entry.Property(nameof(IAuditableEntity<Guid>.LastModifiedBy)).CurrentValue = userId;
                    }

                    // Prevent updating CreatedAt and CreatedBy fields
                    entry.Property(nameof(IAuditableEntity<Guid>.CreatedAt)).IsModified = false;
                    entry.Property(nameof(IAuditableEntity<Guid>.CreatedBy)).IsModified = false;
                }

                if (entry.State == EntityState.Deleted || IsSoftDeleting(entry))
                {
                    entry.Property(nameof(IAuditableEntity<Guid>.DeletedAt)).CurrentValue = utcNow;
                    entry.Property(nameof(IAuditableEntity<Guid>.DeletedBy)).CurrentValue = userId;
                    entry.State = EntityState.Modified;
                }
            }
        }

        private bool IsSoftDeleting(EntityEntry entry)
        {
            return entry.State == EntityState.Modified &&
                   entry.Property(nameof(IAuditableEntity<Guid>.DeletedAt)).CurrentValue != null &&
                   entry.Property(nameof(IAuditableEntity<Guid>.DeletedAt)).OriginalValue == null;
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

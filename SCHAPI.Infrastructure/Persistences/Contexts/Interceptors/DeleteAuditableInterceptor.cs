using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SCHAPI.Domain.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Interceptors
{
    public class DeleteAuditableInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context == null)
            {
                return ValueTask.FromResult(result);
            }

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: IDeleteAuditable delete })
                {
                    continue;
                }

                entry.State = EntityState.Modified;
                delete.AuditDeleteUser = 1;
                delete.AuditDeleteDate = DateTime.Now;
            }

            return ValueTask.FromResult(result);
        }
    }
}

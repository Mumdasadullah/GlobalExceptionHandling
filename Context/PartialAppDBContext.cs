using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace InMemoryDBSpecificationRepositoryUOWProject.Models; // should be same namespace as AppDBContext.cs
public partial class AppDBContext : DbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var idProperty = entity.FindProperty("Id");
            if (idProperty != null && !idProperty.IsPrimaryKey())
            {
                idProperty.ValueGenerated = ValueGenerated.OnAdd;
                idProperty.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }
        }
        ApplySoftDeleteFilter(modelBuilder);
    }

    // Global Query Filter for Soft Delete and IsActive
    private static void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var parameter = Expression.Parameter(entity.ClrType, "e");

            Expression? filter = null;

            var isDeletedProp = entity.FindProperty("IsDeleted");
            if (isDeletedProp != null)
            {
                var deletedExpr = Expression.Equal(
                    Expression.Property(parameter, "IsDeleted"),
                    Expression.Constant(false)
                );
                filter = deletedExpr;
            }

            var isActiveProp = entity.FindProperty("IsActive");
            if (isActiveProp != null)
            {
                var activeExpr = Expression.Equal(
                    Expression.Property(parameter, "IsActive"),
                    Expression.Constant(true, typeof(bool?))
                );

                filter = filter != null
                    ? Expression.AndAlso(filter, activeExpr)
                    : activeExpr;
            }

            if (filter != null)
            {
                var lambda = Expression.Lambda(filter, parameter);
                modelBuilder.Entity(entity.ClrType).HasQueryFilter(lambda);
            }

        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInformation()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) continue;
            
            var hasIsDeleted = entry.Metadata.FindProperty("IsDeleted") != null;
            var hasUpdatedOn = entry.Metadata.FindProperty("UpdatedOn") != null;
            var hasIsActive = entry.Metadata.FindProperty("IsActive") != null;

            if (entry.State == EntityState.Deleted && hasIsDeleted) {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
                entry.CurrentValues["DeletedOn"] = DateTime.UtcNow;
            } else if (entry.State == EntityState.Modified && hasUpdatedOn) {
                entry.CurrentValues["UpdatedOn"] = DateTime.UtcNow;
            } else if (entry.State == EntityState.Added && hasIsActive) {
                entry.CurrentValues["EntityId"] = entry.CurrentValues["EntityId"].ToString() == Guid.Empty.ToString() ? Guid.NewGuid() : entry.CurrentValues["EntityId"];
                if (entry.CurrentValues["IsActive"] == null)
                    entry.CurrentValues["IsActive"] = true;
            }
        }
    }
}

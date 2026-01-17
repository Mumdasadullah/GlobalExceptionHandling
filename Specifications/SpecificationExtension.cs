using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public static class SpecificationExtension
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity>(
            this IQueryable<TEntity> query,
            ISpecification<TEntity> specification) where TEntity : BaseEntity
        {
            var efCoreSpecification = new EfCoreSpecification<TEntity>(specification);

            query = query.AsNoTracking();
            //var query = _context.Set<TEntity>().AsNoTracking();
            query = efCoreSpecification.Apply(query);

            return query;
        }
    }
}

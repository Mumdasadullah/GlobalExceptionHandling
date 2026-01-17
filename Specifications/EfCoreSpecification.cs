using InMemoryDBSpecificationRepositoryUOWProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public class EfCoreSpecification<TEntity> : Specification<TEntity> where TEntity : BaseEntity
    {
        public EfCoreSpecification(ISpecification<TEntity> specification) : base(specification)
        {
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryable)
        {
            if (Criteria is not null)
            {
                queryable = queryable.Where(Criteria);
            }

            if (IncludesQueries?.Count > 0)
            {
                queryable = IncludesQueries.Aggregate(queryable, (current, include) => current.Include(include));
            }

            if (OrderByQueries?.Count > 0)
            {
                var orderByQueryable = queryable.OrderBy(OrderByQueries.First());

                orderByQueryable = OrderByQueries.Skip(1)
                    .Aggregate(orderByQueryable, (current, orderBy) => current.ThenBy(orderBy));

                queryable = orderByQueryable;
            }
            
            if (OrderByDescendingQueries?.Count > 0)
            {
                var orderByDescQueryable = queryable.OrderByDescending(OrderByDescendingQueries.First());

                orderByDescQueryable = OrderByDescendingQueries.Skip(1)
                    .Aggregate(orderByDescQueryable, (current, orderByDescending) => current.ThenByDescending(orderByDescending));

                queryable = orderByDescQueryable;
            }

            return queryable;
        }
    }
}

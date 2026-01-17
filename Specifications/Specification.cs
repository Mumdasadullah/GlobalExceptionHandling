using System.Linq.Expressions;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public interface ISpecification<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        IReadOnlyCollection<Expression<Func<TEntity, object>>> IncludesQueries { get; }
        IReadOnlyCollection<Expression<Func<TEntity, object>>> OrderByQueries { get; }
        IReadOnlyCollection<Expression<Func<TEntity, object>>> OrderByDescendingQueries { get; }
    }

    public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
    {
        private List<Expression<Func<TEntity, object>>> _includesQueries = new();
        private List<Expression<Func<TEntity, object>>> _orderByQueries = new();
        private List<Expression<Func<TEntity, object>>> _orderByDescendingQueries = new();

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public IReadOnlyCollection<Expression<Func<TEntity, object>>> IncludesQueries => _includesQueries.AsReadOnly();
        public IReadOnlyCollection<Expression<Func<TEntity, object>>> OrderByQueries => _orderByQueries.AsReadOnly();
        public IReadOnlyCollection<Expression<Func<TEntity, object>>> OrderByDescendingQueries => _orderByDescendingQueries.AsReadOnly();

        protected Specification() { }

        protected Specification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected Specification(ISpecification<TEntity> specification)
        {
            Criteria = specification.Criteria;
            _includesQueries = specification.IncludesQueries?.ToList() ?? new();
            _orderByQueries = specification.OrderByQueries?.ToList() ?? new();
            _orderByDescendingQueries = specification.OrderByDescendingQueries?.ToList() ?? new();
        }

        protected void AddFilterQuery(Expression<Func<TEntity, bool>> query)
        {
            Criteria = query;
        }

        protected void AddIncludeQuery(Expression<Func<TEntity, object>> includeQuery)
        {
            _includesQueries.Add(includeQuery);
        }

        protected void AddOrderByQuery(Expression<Func<TEntity, object>> orderByQuery)
        {
            _orderByQueries.Add(orderByQuery);
        }

        protected void AddOrderByDescendingQuery(Expression<Func<TEntity, object>> orderByDescendingQuery)
        {
            _orderByDescendingQueries.Add(orderByDescendingQuery);
        }
    }
}

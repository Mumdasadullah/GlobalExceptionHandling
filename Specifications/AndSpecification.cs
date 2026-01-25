using System.Linq.Expressions;
using InMemoryDBSpecificationRepositoryUOWProject.Helpers;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public class AndSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        public AndSpecification(Specification<TEntity> left, Specification<TEntity> right)
        {
            RegisterFilterQuery(left, right);
        }

        private void RegisterFilterQuery(Specification<TEntity> left, Specification<TEntity> right)
        {
            var leftCriteria = left.Criteria;
            var rightCriteria = right.Criteria;

            if (leftCriteria is null && rightCriteria is null)
                return;

            if (leftCriteria is not null && rightCriteria is null)
            {
                AddFilterQuery(leftCriteria);
                return;
            }

            if (leftCriteria is null && rightCriteria is not null)
            {
                AddFilterQuery(rightCriteria);
                return;
            }

            var replaceVisitor = new ReplaceExpressionVisitor(rightCriteria!.Parameters.Single(), leftCriteria!.Parameters.Single());
            var replaceBody = replaceVisitor.Visit(rightCriteria.Body);

            var andCriteria = Expression.AndAlso(leftCriteria.Body, replaceBody);
            var lamdba = Expression.Lambda<Func<TEntity, bool>>(andCriteria, leftCriteria.Parameters.Single());

            AddFilterQuery(lamdba);
        }
    }
}

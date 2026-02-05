using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public class UserSpecifications
    {
    }

    public class GetUserByIdInfo : Specification<User>
    {
        public GetUserByIdInfo(Guid Id)
        {
            AddFilterQuery(user => user.EntityId == Id);
        }
    }
}

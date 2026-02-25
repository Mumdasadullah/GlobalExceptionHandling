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

    public class GetUserByEmail : Specification<User>
    {
        public GetUserByEmail(string email)
        {
            AddFilterQuery(user => user.Email == email);
        }
    }
}

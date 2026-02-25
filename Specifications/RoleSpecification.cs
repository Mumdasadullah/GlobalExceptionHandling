using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public class RoleSpecification
    {
    }

    public class GetRoleByName : Specification<Role>
    {
        public GetRoleByName(string name)
        {
            AddFilterQuery(role => role.Name.ToLower() == name.ToLower());
        }
    }
    public class GetRoleById : Specification<Role>
    {
        public GetRoleById(Guid Id)
        {
            AddFilterQuery(role => role.EntityId == Id);
        }
    }
}

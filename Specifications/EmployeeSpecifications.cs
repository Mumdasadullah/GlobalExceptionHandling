using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public class EmployeeSpecifications : Specification<Employee>
    {
        public EmployeeSpecifications(string companyName)
        {
            AddFilterQuery(emp => emp.Company!.Name == companyName);
            AddIncludeQuery(emp => emp.Company!);
            AddOrderByQuery(emp => emp.Id);
        }
    }
    public class GetEmployeeByIdInfo : Specification<Employee>
    {
        public GetEmployeeByIdInfo(Guid Id)
        {
            AddFilterQuery(emp => emp.EntityId == Id);
            AddIncludeQuery(emp => emp.Company!);
            AddIncludeQuery(emp => emp.CreatedByNavigation!);
        }
    }
}

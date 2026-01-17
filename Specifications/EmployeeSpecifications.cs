using InMemoryDBSpecificationRepositoryUOWProject.DTOs.EmployeeDTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public class EmployeeSpecifications : Specification<Employee>
    {
        public EmployeeSpecifications(string status)
        {
            AddFilterQuery(emp => emp.Status!.Name == status);
            AddIncludeQuery(emp => emp.Status!);
            AddIncludeQuery(emp => emp.Country!);
            AddIncludeQuery(emp => emp.Province!);
            AddIncludeQuery(emp => emp.City!);
            AddOrderByQuery(emp => emp.Id);
        }
    }
    public class GetEmployeeByIdInfo : Specification<Employee>
    {
        public GetEmployeeByIdInfo(int Id)
        {
            AddFilterQuery(emp => emp.Id == Id);
            AddIncludeQuery(emp => emp.Status!);
            AddIncludeQuery(emp => emp.Country!);
            AddIncludeQuery(emp => emp.Province!);
            AddIncludeQuery(emp => emp.City!);
        }
    }
}

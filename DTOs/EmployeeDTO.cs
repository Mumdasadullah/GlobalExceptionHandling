using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string CreatedOn { get; set; } = null!;
    }

    public static class EmployeeExtensionMethod
    {
        public static EmployeeDTO ToEmployeeDTO(this Employee employee)
        {
            return new EmployeeDTO
            {
                Name = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                Company = employee.Company?.Name ?? string.Empty,
                CreatedBy = employee.CreatedByNavigation?.Name ?? string.Empty,
                CreatedOn = employee.CreatedDate.ToString("MMM dd yyyy hh:mm:ss")
            };
        }
    }
}

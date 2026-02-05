using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs
{
    public class EmployeeDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CNIC { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string CreatedOn { get; set; } = null!;
    }

    public class AddEmployeeDTO
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CNIC { get; set; } = null!;
        public Guid CompanyId { get; set; }
    }

    public static class EmployeeExtensionMethod
    {
        public static EmployeeDTO ToEmployeeDTO(this Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.EntityId.ToString(),
                Name = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                CNIC = employee.Cnic,
                Company = employee.Company?.Name ?? string.Empty,
                CreatedBy = employee.CreatedByNavigation?.Name ?? string.Empty,
                CreatedOn = employee.CreatedDate.ToString("MMM dd yyyy hh:mm:ss")
            };
        }
    }
}

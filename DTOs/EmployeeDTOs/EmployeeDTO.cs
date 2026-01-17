using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Status { get; set; } = null!;
    }

    public static class EmployeeExtensionMethod
    {
        public static EmployeeDTO ToEmployeeDTO(this Employee employee)
        {
            return new EmployeeDTO
            {
                Name = $"{employee.FirstName} {employee.LastName}",
                UserName = employee.UserName,
                Email = employee.Email,
                Country = employee.Country?.Name ?? string.Empty,
                Province = employee.Province?.Name ?? string.Empty,
                City = employee.City?.Name ?? string.Empty,
                Status = employee.Status?.Name ?? string.Empty
            };
        }
    }
}

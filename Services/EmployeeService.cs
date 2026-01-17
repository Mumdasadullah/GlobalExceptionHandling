using InMemoryDBSpecificationRepositoryUOWProject.DTOs.EmployeeDTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Respositories;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<EmployeeDTO> GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            return employees;
        }
        public EmployeeDTO GetEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee;
        }
    }
}

using InMemoryDBSpecificationRepositoryUOWProject.DTOs.EmployeeDTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Exceptions;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using InMemoryDBSpecificationRepositoryUOWProject.UnitOfWork;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<EmployeeDTO> GetEmployees()
        {
            var spec = new EmployeeSpecifications("Active");
            var employees = _unitOfWork.EmployeeRead.GetAll(spec);
            List<EmployeeDTO> response = new();
            foreach (var emp in employees)
            {
                response.Add(emp.ToEmployeeDTO());
            }
            return response;
        }
        public EmployeeDTO GetEmployee(int id)
        {
            var spec = new GetEmployeeByIdInfo(id);
            var employee = _unitOfWork.EmployeeRead.GetById(spec);
            if (employee == null)
                throw new NotFoundException("No Employee Found");
            return employee.ToEmployeeDTO();
        }
    }
}

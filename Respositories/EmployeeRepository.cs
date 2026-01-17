using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.DTOs.EmployeeDTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;

namespace InMemoryDBSpecificationRepositoryUOWProject.Respositories
{
    public interface IEmployeeRepository
    {
        List<EmployeeDTO> GetEmployees();
        EmployeeDTO GetById(int id);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly InMemoryDBContext _context;

        public EmployeeRepository(InMemoryDBContext context)
        {
            _context = context;
        }

        public List<EmployeeDTO> GetEmployees()
        {
            var spec = new EmployeeSpecifications("Active");
            return _context.ApplySpecification(spec).Select(x => x.ToEmployeeDTO()).ToList();
        }
        public EmployeeDTO GetById(int id)
        {
            var spec = new GetEmployeeByIdInfo(id);
            return _context.ApplySpecification(spec).Select(x => x.ToEmployeeDTO()).FirstOrDefault() ?? new();
        }
    }
}

using InMemoryDBSpecificationRepositoryUOWProject.DTOs.EmployeeDTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Exceptions;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using InMemoryDBSpecificationRepositoryUOWProject.UnitOfWork;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(int id);
        bool EmployeeAndCountryAdd();
        bool EmployeeUpdate();
        bool EmployeeDelete();
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

        public bool EmployeeAndCountryAdd()
        {
            var spec = new StatusSpecification(1);
            Status active = _unitOfWork.StatusRead.GetById(spec);
            if (active == null)
                throw new NotFoundException("No Status Found");
            var bangladesh = new Country { Id = 3, Name = "Bangladesh" };
            var tamim = new Employee { Id = 5, FirstName = "Tamim", LastName = "Iqbal", UserName = "Tamim22115*", Email = "tamimiqbal@test.com", Country = bangladesh, Status = active };
            _unitOfWork.StatusWrite.Attach(active);
            _unitOfWork.CountryWrite.Insert(bangladesh);
            _unitOfWork.EmployeeWrite.Insert(tamim);
            _unitOfWork.SaveChanges();
            return true;
        }
        public bool EmployeeUpdate()
        {
            var empSpec = new GetEmployeeByIdInfo(1);
            Employee emp = _unitOfWork.EmployeeRead.GetById(empSpec);
            if (emp == null)
                throw new NotFoundException("No Employee Found");
            var countrySpec = new CountrySpecification(1);
            Country pakistan = _unitOfWork.CountryRead.GetById(countrySpec);
            if (pakistan == null)
                throw new NotFoundException("No Country Found");
            emp.FirstName = "Qasim";
            emp.LastName = "Ali";
            emp.UserName = "Qasim22115*";
            emp.Email = "qasimali@test.com";
            var gilgit = new Province { Id = 3, Name = "Gilgit", Country = pakistan };
            emp.Province = gilgit;
            _unitOfWork.CountryWrite.Attach(pakistan);
            _unitOfWork.ProvinceWrite.Insert(gilgit);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool EmployeeDelete()
        {
            var empSpec = new GetEmployeeByIdInfo(1);
            Employee emp = _unitOfWork.EmployeeRead.GetById(empSpec);
            if (emp == null)
                throw new NotFoundException("No Employee Found");
            _unitOfWork.CountryWrite.Delete(emp.Country!);
            _unitOfWork.ProvinceWrite.Delete(emp.Province!);
            _unitOfWork.CityWrite.Delete(emp.City!);
            _unitOfWork.EmployeeWrite.Delete(emp);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}

using System.Threading.Tasks;
using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Exceptions;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using InMemoryDBSpecificationRepositoryUOWProject.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetEmployees();
        Task<EmployeeDTO> GetEmployee(Guid id);
        //Task<bool> EmployeeAndCountryAdd();
        //Task<bool> EmployeeUpdate();
        //Task<bool> EmployeeDelete();
        Task<EmployeeDTO> AddEmployee(AddEmployeeDTO request);
        Task<EmployeeDTO> DeleteEmployee(Guid Id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDBContext _context;

        public EmployeeService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            //var spec = new EmployeeSpecifications("Active");
            //var employees = _unitOfWork.EmployeeRead.GetAll(spec);
            //List<EmployeeDTO> response = new();
            //foreach (var emp in employees)
            //{
            //    response.Add(emp.ToEmployeeDTO());
            //}
            //return response;
            List<EmployeeDTO> response = await _context.Employees.Select(emp => emp.ToEmployeeDTO()).ToListAsync();
            return response;
        }
        public async Task<EmployeeDTO> GetEmployee(Guid id)
        {
            var spec = new GetEmployeeByIdInfo(id);
            //var employee = _unitOfWork.EmployeeRead.GetById(spec);
            //if (employee == null)
            //    throw new NotFoundException("No Employee Found");
            //return employee.ToEmployeeDTO();
            EmployeeDTO response = await _context.Employees.ApplySpecification(spec).Select(emp => emp.ToEmployeeDTO()).FirstOrDefaultAsync() ?? throw new NotFoundException("No Employee Found");
            return response;
        }

        public async Task<EmployeeDTO> AddEmployee(AddEmployeeDTO request)
        {
            bool isEmployeeExistOrNot = await _context.Employees.AnyAsync(x => x.Email.ToLower() == request.Email.ToLower() || x.Cnic == request.CNIC);
            if (isEmployeeExistOrNot)
                throw new ConflictException("CNIC or Email Duplicates; Employee Already Exists");
            var spec = new GetCompanyByIdInfo(request.CompanyId);
            Company company = await _context.Companies.ApplySpecification(spec).FirstOrDefaultAsync() ?? throw new NotFoundException("No Company Found");
            Employee employee = new() { FirstName = request.FirstName, MiddleName = request.MiddleName, LastName = request.LastName, Email = request.Email, Cnic = request.CNIC, CompanyId = request.CompanyId, CreatedBy = Guid.Parse("C7993C8B-0AF6-45F3-87AB-070AFBB2C703") };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return await GetEmployee(employee.EntityId);
        }

        public async Task<EmployeeDTO> DeleteEmployee(Guid Id)
        {
            try
            {
                var spec = new GetEmployeeByIdInfo(Id);
                Employee employee = await _context.Employees.ApplySpecification(spec).FirstOrDefaultAsync() ?? throw new NotFoundException("No Employee Found");
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return employee.ToEmployeeDTO();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<bool> EmployeeAndCountryAdd()
        //{
        //    Status active = _unitOfWork.StatusRead.GetById(spec);
        //    if (active == null)
        //        throw new NotFoundException("No Status Found");
        //    var bangladesh = new Country { Id = 3, Name = "Bangladesh" };
        //    var tamim = new Employee { Id = 5, FirstName = "Tamim", LastName = "Iqbal", UserName = "Tamim22115*", Email = "tamimiqbal@test.com", Country = bangladesh, Status = active };
        //    _unitOfWork.StatusWrite.Attach(active);
        //    _unitOfWork.CountryWrite.Insert(bangladesh);
        //    _unitOfWork.EmployeeWrite.Insert(tamim);
        //    _unitOfWork.SaveChanges();
        //    return true;
        //}
        //public bool EmployeeUpdate()
        //{
        //    var empSpec = new GetEmployeeByIdInfo(1);
        //    Employee emp = _unitOfWork.EmployeeRead.GetById(empSpec);
        //    if (emp == null)
        //        throw new NotFoundException("No Employee Found");
        //    var countrySpec = new CountrySpecification(1);
        //    Country pakistan = _unitOfWork.CountryRead.GetById(countrySpec);
        //    if (pakistan == null)
        //        throw new NotFoundException("No Country Found");
        //    emp.FirstName = "Qasim";
        //    emp.LastName = "Ali";
        //    emp.UserName = "Qasim22115*";
        //    emp.Email = "qasimali@test.com";
        //    var gilgit = new Province { Id = 3, Name = "Gilgit", Country = pakistan };
        //    emp.Province = gilgit;
        //    _unitOfWork.CountryWrite.Attach(pakistan);
        //    _unitOfWork.ProvinceWrite.Insert(gilgit);
        //    _unitOfWork.SaveChanges();
        //    return true;
        //}

        //public bool EmployeeDelete()
        //{
        //    var empSpec = new GetEmployeeByIdInfo(1);
        //    Employee emp = _unitOfWork.EmployeeRead.GetById(empSpec);
        //    if (emp == null)
        //        throw new NotFoundException("No Employee Found");
        //    _unitOfWork.CountryWrite.Delete(emp.Country!);
        //    _unitOfWork.ProvinceWrite.Delete(emp.Province!);
        //    _unitOfWork.CityWrite.Delete(emp.City!);
        //    _unitOfWork.EmployeeWrite.Delete(emp);
        //    _unitOfWork.SaveChanges();
        //    return true;
        //}
    }
}

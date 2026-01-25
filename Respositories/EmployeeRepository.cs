using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Respositories
{
    //public interface IEmployeeReadRepository : IGenericReadRepository<Employee>
    //{
    //}
    //public interface IEmployeeWriteRepository : IGenericWriteRepository<Employee>
    //{
    //}

    //public class EmployeeReadRepository : GenericReadRepository<Employee>, IEmployeeReadRepository
    //{
    //    public EmployeeReadRepository(InMemoryDBContext context) : base(context)
    //    {
    //    }
    //    public InMemoryDBContext InMemoryDBContext
    //    {
    //        get { return _context as InMemoryDBContext; }
    //    }
    //}
    //public class EmployeeWriteRepository : GenericWriteRepository<Employee>,  IEmployeeWriteRepository
    //{
    //    public EmployeeWriteRepository(InMemoryDBContext context) : base(context)
    //    {
    //    }
    //    public InMemoryDBContext InMemoryDBContext
    //    {
    //        get { return _context as InMemoryDBContext; }
    //    }
    //}
}

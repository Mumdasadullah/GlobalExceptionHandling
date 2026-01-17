using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.Respositories;

namespace InMemoryDBSpecificationRepositoryUOWProject.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeReadRepository EmployeeRead { get; }
        IEmployeeWriteRepository EmployeeWrite { get; }
        int SaveChanges();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InMemoryDBContext _context;
        public IEmployeeReadRepository EmployeeRead { get; }
        public IEmployeeWriteRepository EmployeeWrite { get; }

        public UnitOfWork(InMemoryDBContext context)
        {
            _context = context;
            EmployeeRead = new EmployeeReadRepository(_context);
            EmployeeWrite = new EmployeeWriteRepository(_context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

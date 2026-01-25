using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.Respositories;

namespace InMemoryDBSpecificationRepositoryUOWProject.UnitOfWork
{
    //public interface IUnitOfWork : IDisposable
    //{
    //    IEmployeeReadRepository EmployeeRead { get; }
    //    IEmployeeWriteRepository EmployeeWrite { get; }
    //    ICountryReadRepository CountryRead { get; }
    //    ICountryWriteRepository CountryWrite { get; }
    //    IStatusReadRepository StatusRead { get; }
    //    IStatusWriteRepository StatusWrite { get; }
    //    IProvinceReadRepository ProvinceRead { get; }
    //    IProvinceWriteRepository ProvinceWrite { get; }
    //    ICityReadRepository CityRead { get; }
    //    ICityWriteRepository CityWrite { get; }
    //    int SaveChanges();
    //}
    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly InMemoryDBContext _context;
    //    public IEmployeeReadRepository EmployeeRead { get; }
    //    public IEmployeeWriteRepository EmployeeWrite { get; }
    //    public ICountryReadRepository CountryRead { get; }
    //    public ICountryWriteRepository CountryWrite { get; }
    //    public IStatusReadRepository StatusRead { get; }
    //    public IStatusWriteRepository StatusWrite { get; }
    //    public IProvinceReadRepository ProvinceRead { get; }
    //    public IProvinceWriteRepository ProvinceWrite { get; }
    //    public ICityReadRepository CityRead { get; }
    //    public ICityWriteRepository CityWrite { get; }
    //    private bool _disposed = false;
    //    public UnitOfWork(InMemoryDBContext context)
    //    {
    //        _context = context;
    //        EmployeeRead = new EmployeeReadRepository(_context);
    //        EmployeeWrite = new EmployeeWriteRepository(_context);
    //        CountryRead = new CountryReadRepository(_context);
    //        CountryWrite = new CountryWriteRepository(_context);
    //        StatusRead = new StatusReadRepository(_context);
    //        StatusWrite = new StatusWriteRepository(_context);
    //        ProvinceRead = new ProvinceReadRepository(_context);
    //        ProvinceWrite = new ProvinceWriteRepository(_context);
    //        CityRead = new CityReadRepository(_context);
    //        CityWrite = new CityWriteRepository(_context);
    //    }

    //    public int SaveChanges()
    //    {
    //        return _context.SaveChanges();
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //    protected virtual void Dispose(bool disposed)
    //    {
    //        if (!_disposed)
    //        {
    //            if (disposed)
    //            {
    //                _context.Dispose();
    //            }
    //            _disposed = true;
    //        }
    //    }
    //}
}

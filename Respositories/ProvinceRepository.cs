using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Respositories
{
    public interface IProvinceReadRepository : IGenericReadRepository<Province> { }
    public interface IProvinceWriteRepository : IGenericWriteRepository<Province> { }
    public class ProvinceReadRepository : GenericReadRepository<Province>, IProvinceReadRepository
    {
        public ProvinceReadRepository(InMemoryDBContext context) : base(context) { }
        public InMemoryDBContext InMemoryDBContext { get { return _context as InMemoryDBContext; } }
    }
    public class ProvinceWriteRepository : GenericWriteRepository<Province>, IProvinceWriteRepository
    {
        public ProvinceWriteRepository(InMemoryDBContext context) : base(context) { }
        public InMemoryDBContext InMemoryDBContext { get { return _context as InMemoryDBContext; } }
    }
}

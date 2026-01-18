using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Respositories
{
    public interface IStatusReadRepository : IGenericReadRepository<Status>
    {
        
    }
    public interface IStatusWriteRepository : IGenericWriteRepository<Status>
    {

    }
    public class StatusReadRepository : GenericReadRepository<Status>, IStatusReadRepository
    {
        public StatusReadRepository(InMemoryDBContext context) : base(context)
        {
        }
        public InMemoryDBContext InMemoryDBContext
        {
            get { return _context as InMemoryDBContext; }
        }
    }
    public class StatusWriteRepository : GenericWriteRepository<Status>, IStatusWriteRepository
    {
        public StatusWriteRepository(InMemoryDBContext context) : base(context)
        {
        }
        public InMemoryDBContext InMemoryDBContext
        {
            get { return _context as InMemoryDBContext; }
        }
    }
}

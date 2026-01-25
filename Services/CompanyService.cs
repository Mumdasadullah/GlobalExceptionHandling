using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using InMemoryDBSpecificationRepositoryUOWProject.Exceptions;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface ICompanyService
    {
        Task<bool> AddCompany(AddCompanyDTO company);
    }
    public class CompanyService : ICompanyService
    {
        private readonly AppDBContext _context;

        public CompanyService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCompany(AddCompanyDTO company)
        {
            var spec = new GetUserByIdInfo(company.CreatedBy);
            User user = _context.Users.ApplySpecification(spec).FirstOrDefault() ?? throw new NotFoundException("No User Found");
            Company _company = new()
            {
                CompanyId = Guid.NewGuid(),
                Name = company.Name,
                Description = company.Description,
                CreatedBy = user.UserId
            };

            await _context.Companies.AddAsync(_company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using Microsoft.EntityFrameworkCore;
using InMemoryDBSpecificationRepositoryUOWProject.Exceptions;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface IRoleService
    {
        Task<bool> AddRole(AddRoleDTO role);
    }
    public class RoleService : IRoleService
    {
        private readonly AppDBContext _context;

        public RoleService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRole(AddRoleDTO role)
        {
            var spec = new GetRoleByName(role.Name);
            var existingRole = await _context.Roles.ApplySpecification(spec).FirstOrDefaultAsync();
            if (existingRole is not null) throw new ConflictException("Role is already exists");
            Role newRole = new()
            {
                Name = role.Name,
                Description = role.Description,
                CreatedBy = role.CreatedBy
            };
            await _context.Roles.AddAsync(newRole);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

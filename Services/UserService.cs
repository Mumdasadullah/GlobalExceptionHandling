using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(AddUserDTO user);
    }
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;
        public UserService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(AddUserDTO user)
        {
            User _user = new()
            {
                UserId = Guid.NewGuid(),
                Name = user.Name,
                Description = user.Description
            };
            await _context.Users.AddAsync(_user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

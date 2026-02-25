using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BCrypt.Net;
using InMemoryDBSpecificationRepositoryUOWProject.AppSettingsModels;
using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Exceptions;
using InMemoryDBSpecificationRepositoryUOWProject.Helpers;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InMemoryDBSpecificationRepositoryUOWProject.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(AddUserDTO user);
        Task<bool> RegisterUser(RegisterUserDTO register);
        Task<bool> AssignRoles(AssignUserRoleDTO userRoles);
        Task<string> LoginUser(LoginUserRequestDTO request);
    }
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;
        private readonly JWTSettings _jwtSettings;
        public UserService(AppDBContext context, JWTSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public async Task<bool> AddUser(AddUserDTO user)
        {
            User _user = new()
            {
                Name = user.Name,
                Description = user.Description
            };
            await _context.Users.AddAsync(_user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegisterUser(RegisterUserDTO register)
        {
            var spec = new GetUserByEmail(register.Email);
            var existingUser = await _context.Users.ApplySpecification(spec).FirstOrDefaultAsync();
            if (existingUser is not null) throw new ConflictException("User Already Exists");
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);
            User user = new()
            {
                Name = register.Name,
                Description = register.Description,
                Email = register.Email,
                Password = passwordHash
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var token = GenerateJwtToken(user);
            return true;
        }

        public async Task<string> LoginUser(LoginUserRequestDTO request)
        {
            //var spec = new GetUserByEmail(request.Email);
            //var user = await _context.Users.ApplySpecification(spec).FirstOrDefaultAsync();
            //if (user is null) throw new NotFoundException("No User Found with this email");
            //bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            //if (!isPasswordValid) throw new UnauthorizedAccessException("Invalid Password");
            User? user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user is null) throw new NotFoundException("Invalid Email or Password");
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordCorrect) throw new NotFoundException("Invalid Email or Password");
            var token = GenerateJwtToken(user);
            return token;
        }

        public async Task<bool> AssignRoles(AssignUserRoleDTO userRoles)
        {
            bool user = await _context.Users.AnyAsync(x => x.EntityId == userRoles.UserId);
            if (!user) throw new NotFoundException("No User Found");
            List<UserRole> userRole = new();
            foreach (var item in userRoles.Roles)
            {
                bool role = await _context.Roles.AnyAsync(x => x.EntityId == item);
                if (!role) throw new NotFoundException($"No Role Found with Id {item}");
                userRole.Add(new UserRole { EntityId = Guid.NewGuid(), UserId = userRoles.UserId, RoleId = item, CreatedBy = userRoles.CreatedBy });
            }
            await _context.UserRoles.AddRangeAsync(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var rsa = JWTHelper.LoadRSAKeys(_jwtSettings.RSAKeyPath);
            var signingCredentials = new SigningCredentials
            (
                new RsaSecurityKey(rsa),
                SecurityAlgorithms.RsaSha256
            );
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
            };
            foreach (var userRole in user.UserRoleUsers)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        Task<LoginUserResponseDTO> LoginUser(LoginUserRequestDTO request);
        Guid CheckTokenAndClaims();
        Task<string> Refresh(string RefreshToken);
        Task<bool> Revoke(string RefreshToken);
    }
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly ClaimService _claimService;
        public UserService(AppDBContext context, JWTSettings jwtSettings, IWebHostEnvironment environment, ClaimService claimService)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _environment = environment;
            _claimService = claimService;
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

        public async Task<LoginUserResponseDTO> LoginUser(LoginUserRequestDTO request)
        {
            User? user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user is null) throw new NotFoundException("Invalid Email or Password");
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordCorrect) throw new NotFoundException("Invalid Email or Password");
            string token = GenerateJwtToken(user);
            string refreshToken = GenerateRfreshToken();
            RefreshToken refresh = new()
            {
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                UserId = user.EntityId
            };
            await _context.RefreshTokens.AddAsync(refresh);
            await _context.SaveChangesAsync();
            LoginUserResponseDTO response = new() { Token = token, RefreshToken = refreshToken };
            return response;
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

        public Guid CheckTokenAndClaims()
        {
            if (!Guid.TryParse(_claimService["sid"], out Guid UserId)) throw new NotFoundException("Invalid Token");
            return UserId;
        }

        public async Task<string> Refresh(string RefreshToken)
        {
            RefreshToken? refreshToken = await _context.RefreshTokens.Where(x => x.Token == RefreshToken).FirstOrDefaultAsync();
            if (refreshToken is null || refreshToken.ExpiryDate < DateTime.UtcNow || refreshToken.IsRevoked) throw new UnauthorizedException("Unauthorizated User");
            User? user = await _context.Users.Where(x => x.EntityId == refreshToken.UserId).FirstOrDefaultAsync();
            if (user is null) throw new NotFoundException("No User Found");
            return GenerateJwtToken(user);
        }

        public async Task<bool> Revoke(string RefreshToken)
        {
            RefreshToken? refreshToken = await _context.RefreshTokens.Where(x => x.Token == RefreshToken).FirstOrDefaultAsync();
            if (refreshToken is null || refreshToken.ExpiryDate < DateTime.UtcNow || refreshToken.IsRevoked) throw new UnauthorizedException("Unauthorizated User");
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            string path = Path.Combine(_environment.ContentRootPath, _jwtSettings.RSAKeyPath);
            var rsa = JWTHelper.LoadRSAKeys(path);
            var signingCredentials = new SigningCredentials
            (
                new RsaSecurityKey(rsa),
                SecurityAlgorithms.RsaSha256
            );
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.EntityId.ToString()),
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

        private string GenerateRfreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

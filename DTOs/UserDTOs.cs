using System.ComponentModel.DataAnnotations;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs
{
    public class UserDTOs
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
        public string CreatedOn { get; set; } = null!;
    }

    public class AddUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }

    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }

    public class AssignUserRoleDTO
    {
        public Guid UserId { get; set; }
        public List<Guid> Roles { get; set; } = null!;
        public Guid CreatedBy { get; set; }
    }

    public class LoginUserRequestDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginUserResponseDTO
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }

    public static class UserExtensionMethod
    {
        public static UserDTOs toUserDTO(this User user)
        {
            return new UserDTOs
            {
                Id = user.EntityId,
                Email = user.Email,
                Name = user.Name,
                Description = user.Description,
                IsActive = user.IsActive ?? false,
                CreatedOn = user.CreatedDate.ToString("MMM dd yyyy hh:mm:ss")
            };
        }
    }
}

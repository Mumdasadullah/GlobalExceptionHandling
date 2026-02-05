using System.ComponentModel.DataAnnotations;

namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs
{
    public class UserDTOs
    {
    }

    public class AddUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}

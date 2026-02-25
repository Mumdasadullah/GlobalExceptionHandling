using System.ComponentModel.DataAnnotations;

namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs
{
    public class RoleDTOs
    {
    }

    public class AddRoleDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Created By is required")]
        public Guid CreatedBy { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs
{
    public class CompanyDTOs
    {
    }

    public class AddCompanyDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required(ErrorMessage = "CreatedBy is required")]
        public Guid CreatedBy { get; set; }
    }
}

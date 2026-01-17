namespace InMemoryDBSpecificationRepositoryUOWProject.Models
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation Properties
        public Country? Country { get; set; }
        public Province? Province { get; set; }
        public City? City { get; set; }
        public Status? Status { get; set; }
    }
}

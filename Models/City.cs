namespace InMemoryDBSpecificationRepositoryUOWProject.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Country? Country { get; set; }
        public Province? Province { get; set; }
    }
}

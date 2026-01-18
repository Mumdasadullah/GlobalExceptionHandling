using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications
{
    public class CountrySpecification : Specification<Country>
    {
        public CountrySpecification(int Id)
        {
            AddFilterQuery(c => c.Id == Id);
        }
    }
}

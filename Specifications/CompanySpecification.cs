using InMemoryDBSpecificationRepositoryUOWProject.Models;
namespace InMemoryDBSpecificationRepositoryUOWProject.Specifications;
public class CompanySpecification : Specification<Company> { }
public class GetCompanyByIdInfo : Specification<Company>
{
    public GetCompanyByIdInfo(Guid Id)
    {
        AddFilterQuery(comp => comp.EntityId == Id);
        AddIncludeQuery(comp => comp.CreatedByNavigation!);
    }
}

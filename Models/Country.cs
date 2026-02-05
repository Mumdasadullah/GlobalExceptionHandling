using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class Country
{
    public int Id { get; set; }

    public Guid EntityId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}

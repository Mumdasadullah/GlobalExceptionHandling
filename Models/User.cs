using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid EntityId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}

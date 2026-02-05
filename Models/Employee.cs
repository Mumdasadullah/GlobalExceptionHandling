using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class Employee
{
    public int Id { get; set; }

    public Guid EntityId { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Cnic { get; set; }

    public bool? IsActive { get; set; }

    public Guid CompanyId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual Company Company { get; set; }

    public virtual User CreatedByNavigation { get; set; }
}

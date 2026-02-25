using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class Module
{
    public int Id { get; set; }

    public Guid EntityId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool? IsActive { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual User CreatedByNavigation { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual User UpdatedByNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class RolePermission
{
    public int Id { get; set; }

    public Guid RoleId { get; set; }

    public Guid ModuleId { get; set; }

    public Guid PermissionId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual User CreatedByNavigation { get; set; }

    public virtual Module Module { get; set; }

    public virtual Permission Permission { get; set; }

    public virtual Role Role { get; set; }
}

using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public Guid EntityId { get; set; }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual User CreatedByNavigation { get; set; }

    public virtual Role Role { get; set; }

    public virtual User User { get; set; }
}

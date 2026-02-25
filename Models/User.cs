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

    public string Email { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Module> ModuleCreatedByNavigations { get; set; } = new List<Module>();

    public virtual ICollection<Module> ModuleUpdatedByNavigations { get; set; } = new List<Module>();

    public virtual ICollection<Permission> PermissionCreatedByNavigations { get; set; } = new List<Permission>();

    public virtual ICollection<Permission> PermissionUpdatedByNavigations { get; set; } = new List<Permission>();

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();

    public virtual ICollection<UserRole> UserRoleCreatedByNavigations { get; set; } = new List<UserRole>();

    public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();
}

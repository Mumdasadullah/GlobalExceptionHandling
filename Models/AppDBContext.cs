using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.EntityId).HasName("PK_Company");

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeleteOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Users1");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.EntityId);

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EntityId).HasName("PK_Employee");

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.Cnic)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Company1");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Users1");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.EntityId);

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ModuleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modules_Users");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ModuleUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Modules_Users1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.EntityId);

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PermissionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permissions_Users1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PermissionUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Permissions_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.EntityId);

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Users1");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.ModuleId, e.PermissionId });

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Users");

            entity.HasOne(d => d.Module).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Modules");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Roles");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.EntityId).HasName("PK_Status");

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Status_Company1");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Status_Users1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.EntityId).HasName("PK_Users_1");

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("asadullah22115@gmail.com''");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasDefaultValue("asad22115");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.EntityId);

            entity.Property(e => e.EntityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserRoleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users1");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

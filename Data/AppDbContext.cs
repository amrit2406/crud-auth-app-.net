using auth_app.Models;
using Microsoft.EntityFrameworkCore;

namespace auth_app.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>()
            .HasKey(x => new
            {
                x.UserId,
                x.RoleId
            });

        modelBuilder.Entity<RolePermission>()
            .HasKey(x => new
            {
                x.RoleId,
                x.PermissionId
            });

        base.OnModelCreating(modelBuilder);
    }
}
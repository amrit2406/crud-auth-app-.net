using auth_app.Helpers;
using auth_app.Models;

namespace auth_app.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        // Create Admin Role
        Role? adminRole = db.Roles
            .FirstOrDefault(r => r.Name == "Admin");

        if (adminRole == null)
        {
            adminRole = new Role
            {
                Name = "Admin"
            };

            db.Roles.Add(adminRole);
            db.SaveChanges();
        }

        // Create Permissions
        string[] permissions =
        {
            Permissions.UserView,
            Permissions.UserCreate,
            Permissions.UserEdit,
            Permissions.UserDelete,

            Permissions.ProductView,
            Permissions.ProductCreate,
            Permissions.ProductEdit,
            Permissions.ProductDelete
        };

        foreach (var permissionName in permissions)
        {
            if (!db.Permissions.Any(p => p.Name == permissionName))
            {
                db.Permissions.Add(
                    new Permission
                    {
                        Name = permissionName
                    });
            }
        }

        db.SaveChanges();

        // Assign all permissions to Admin role
        var allPermissions = db.Permissions.ToList();

        foreach (var permission in allPermissions)
        {
            bool exists =
                db.RolePermissions.Any(rp =>
                    rp.RoleId == adminRole.Id &&
                    rp.PermissionId == permission.Id);

            if (!exists)
            {
                db.RolePermissions.Add(
                    new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = permission.Id
                    });
            }
        }

        db.SaveChanges();

        // Create Admin User
        User? adminUser = db.Users
            .FirstOrDefault(u => u.Username == "admin");

        if (adminUser == null)
        {
            adminUser = new User
            {
                Username = "admin",
                PasswordHash =
                    PasswordHelper.HashPassword("Admin123")
            };

            db.Users.Add(adminUser);

            db.SaveChanges();
        }

        // Assign Admin Role to Admin User
        bool userRoleExists =
            db.UserRoles.Any(ur =>
                ur.UserId == adminUser.Id &&
                ur.RoleId == adminRole.Id);

        if (!userRoleExists)
        {
            db.UserRoles.Add(
                new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                });
        }

        db.SaveChanges();
    }
}
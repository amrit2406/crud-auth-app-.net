using Microsoft.EntityFrameworkCore;
using auth_app.Data;
using auth_app.Helpers;
using auth_app.Models;

namespace auth_app.Services;

public class AuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> LoginAsync(
        string username,
        string password)
    {
        var user = await _db.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(
            u => u.Username == username);

        if (user == null)
            return null;

        bool valid =
            PasswordHelper.VerifyPassword(
                password,
                user.PasswordHash);

        if (!valid)
            return null;

        UserSession.CurrentUser = user;

        UserSession.Permissions =
            user.UserRoles
                .SelectMany(x =>
                    x.Role.RolePermissions)
                .Select(x =>
                    x.Permission.Name)
                .Distinct()
                .ToList();

        return user;
    }
}
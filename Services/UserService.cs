using auth_app.Data;
using auth_app.Models;
using Microsoft.EntityFrameworkCore;

namespace auth_app.Services;

public class UserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _db.Users
            .OrderBy(x => x.Username)
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);

        await _db.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user =
            await _db.Users.FindAsync(id);

        if (user == null)
            return;

        _db.Users.Remove(user);

        await _db.SaveChangesAsync();
    }
}
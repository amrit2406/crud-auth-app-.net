using auth_app.Data;
using auth_app.Models;
using Microsoft.EntityFrameworkCore;

namespace auth_app.Services;

public class ProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _db.Products
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _db.Products
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        _db.Products.Add(product);

        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _db.Products.Update(product);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product =
            await _db.Products.FindAsync(id);

        if (product == null)
            return;

        _db.Products.Remove(product);

        await _db.SaveChangesAsync();
    }
}
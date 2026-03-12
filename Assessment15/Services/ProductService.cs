using Assessment15.Data;
using Assessment15.DTOs;
using Assessment15.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment15.Services;

public class ProductService
{
    private readonly AppDbContext _db;
    public ProductService(AppDbContext db) => _db = db;

    // Pagination + AsNoTracking + Async
    public async Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        var query = _db.Products.AsNoTracking().OrderByDescending(p => p.Id);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (items, total);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        // AsNoTracking for read
        return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreateAsync(ProductCreateDto dto)
    {
        var p = new Product { Name = dto.Name.Trim(), Price = dto.Price };
        _db.Products.Add(p);
        await _db.SaveChangesAsync();
        return p;
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return false;

        p.Name = dto.Name.Trim();
        p.Price = dto.Price;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return false;

        _db.Products.Remove(p);
        await _db.SaveChangesAsync();
        return true;
    }
}
using DAL.Interface;
using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage;

public class CartStorage : IBaseStorage<CartDb>
{
    private readonly ApplicationDbContext _db;

    public CartStorage(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(CartDb item)
    {   
        await _db.ArchiveDb.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(CartDb item)
    {
        _db.ArchiveDb.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<CartDb> Get(Guid id)
    {
        return await _db.ArchiveDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<CartDb> GetAll()
    {
        return _db.ArchiveDb;
    }

    public async Task<CartDb> Update(CartDb item)
    {
        _db.ArchiveDb.Update(item);
        await _db.SaveChangesAsync();
        return item;
    }
}
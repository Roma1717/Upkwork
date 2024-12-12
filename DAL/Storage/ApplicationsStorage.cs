using DAL.Interface;
using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage;

public class ApplicationsStorage : IBaseStorage<ApplicationsDb>
{
    
    public readonly ApplicationDbContext _db;

    public ApplicationsStorage(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(ApplicationsDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(ApplicationsDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }


    public async Task<ApplicationsDb> Get(Guid id)
    {
        return await _db.ApplicationsDb.FirstOrDefaultAsync(x => x.Id == id);
    }


    public IQueryable<ApplicationsDb> GetAll()
    {
        return _db.ApplicationsDb;
    }


    public async Task<ApplicationsDb> Update(ApplicationsDb item)
    {
        _db.ApplicationsDb.Update(item);
        await _db.SaveChangesAsync();

        return item;
    }
    
}
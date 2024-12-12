using DAL.Interface;
using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage;

public class JobsStorage : IBaseStorage<JobsDb>
{
    public readonly ApplicationDbContext _db;

    public JobsStorage(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(JobsDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(JobsDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }


    public async Task<JobsDb> Get(Guid id)
    {
        return await _db.JobsDb.FirstOrDefaultAsync(x => x.Id == id);
    }


    public IQueryable<JobsDb> GetAll()
    {
        return _db.JobsDb;
    }


    public async Task<JobsDb> Update(JobsDb item)
    {
        _db.JobsDb.Update(item);
        await _db.SaveChangesAsync();

        return item;
    }


}
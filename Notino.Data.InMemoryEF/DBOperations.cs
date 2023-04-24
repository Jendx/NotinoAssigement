namespace Notino.Data.InMemoryEF;

using Microsoft.EntityFrameworkCore;
using Notino.Data.InMemoryEF.Database;
using Notino.Domain.Abstraction;
using Notino.Domain.Models.Abstraction;

public sealed class DBOperations<TModel> : IDBOperations<TModel>
    where TModel : class, IModel
{
    private readonly NotinoDBContext _dbContext;

    public DBOperations(NotinoDBContext dBContext)
    {
        _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
    }

    public async Task<IEnumerable<TModel>> GetAsync(TModel parameters, string query = null)
    {
        return await _dbContext.Set<TModel>().Where(d => d.Id == parameters.Id).ToArrayAsync();
    }

    public async Task<TModel> InsertAsync(TModel data)
    {
        var result = await _dbContext.Set<TModel>().AddAsync(data);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<TModel> UpdateAsync(TModel data)
    {
        var entity = await _dbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == data.Id);

        _dbContext.Entry(entity).CurrentValues.SetValues(data);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
}
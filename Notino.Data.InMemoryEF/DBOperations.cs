namespace Notino.Data.InMemoryEF;

using Notino.Domain.Abstraction;
using Notino.Domain.Models.Abstraction;

public sealed class DBOperations<TModel> : IDBOperations<TModel>
    where TModel : IModel
{
    public Task<IEnumerable<TModel>> GetAsync(string query = null, object parameters = null)
    {
        throw new NotImplementedException();
    }

    public Task<TModel> InsertAsync(TModel data)
    {
        throw new NotImplementedException();
    }

    public Task<TModel> UpdateAsync(TModel data)
    {
        throw new NotImplementedException();
    }
}
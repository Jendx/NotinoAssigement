namespace Notino.Data.SQLite;

using Notino.Domain.Abstraction;
using Notino.Domain.Models.Abstraction;
using System.Threading.Tasks;

internal sealed class DBOperations<TModel> : IDBOperations<TModel>
    where TModel : IModel
{
    public TModel Get()
    {
        throw new NotImplementedException();
    }

    public async Task<TModel> UpdateAsync(TModel data)
    {
        throw new NotImplementedException();
    }

    public async Task<TModel> InsertAsync(TModel data)
    {
        throw new NotImplementedException();
    }
}

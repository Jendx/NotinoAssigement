using Notino.Domain.Abstraction;

namespace Notino.Data.MSSQL;

internal sealed class DBOperations<TModel> : IDBOperations<TModel>
{
    public TModel Get()
    {
        throw new NotImplementedException();
    }

    public TModel Insert(TModel data)
    {
        throw new NotImplementedException();
    }

    public TModel Update(TModel data)
    {
        throw new NotImplementedException();
    }
}

namespace Notino.Domain.Abstraction;

public interface IDBOperations<TModel>
{
    public TModel Get();

    public TModel Update(TModel data);
    
    public TModel Insert(TModel data);
}

namespace Notino.Domain.Abstraction;

using Notino.Domain.Models.Abstraction;

public interface IDBOperations<TModel> 
    where TModel : IModel
{
    public TModel Get();

    public Task<TModel> UpdateAsync(TModel data);
    
    public Task<TModel> InsertAsync(TModel data);
}

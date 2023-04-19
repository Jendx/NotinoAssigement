namespace Notino.Domain.Abstraction;

internal interface IHandler<TModel>
{
    public Task<TModel> HandleAsync(TModel model);

}
namespace Notino.Api.Handlers.Abstraction;

using Notino.Domain.Models.Abstraction;

internal interface IHandler<TModel>
    where TModel : IModel
{
    public Task<TModel> HandleAsync(TModel model);
}
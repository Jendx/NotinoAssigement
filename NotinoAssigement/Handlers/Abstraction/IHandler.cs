namespace Notino.Api.Handlers.Abstraction;

using Notino.Domain.Commands.Abstraction;
using Notino.Domain.Models.Abstraction;

internal interface IHandler<TModel, TCommand>
    where TModel : IModel
    where TCommand : ICommand
{
    public Task<TModel> HandleAsync(TCommand model);
}
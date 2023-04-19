namespace NotinoAssigement.Handlers.Abstraction;

internal interface IHandler<TModel>
{
    public TModel HandleAsync(TModel model);

}
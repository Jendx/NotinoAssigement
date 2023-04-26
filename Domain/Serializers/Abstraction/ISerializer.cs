namespace Notino.Domain.Serializers.Abstraction;

using Notino.Domain.Models.Abstraction;

public interface ISerializer<TData>
    where TData : IModel
{
    public string Serialize(TData data);
}

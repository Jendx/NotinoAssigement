using Notino.Domain.Models.Abstraction;

namespace Notino.Domain.Serializers.Abstraction;

public interface ISerializer<TData>
    where TData : IModel
{
    public string Serialize(TData data);
}

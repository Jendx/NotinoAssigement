namespace Notino.Domain.Serializers;

using Notino.Domain.Models.Abstraction;
using Notino.Domain.Serializers.Abstraction;
using System.Text.Json;

internal sealed class JsonSerializer<TData> : ISerializer<TData>
    where TData : IModel
{
    private readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

   public string Serialize(TData data) => JsonSerializer.Serialize(data, options);
}

using Notino.Domain.Enums;
using Notino.Domain.Models.Abstraction;
using Notino.Domain.Serializers.Abstraction;

namespace Notino.Domain.Serializers;

public static class SerializerFactory
{
    public static ISerializer<TData> CreateSerializer<TData>(DocumentType documentType)
        where TData : IModel
    {
        return documentType switch
        {
            DocumentType.Json => new JsonSerializer<TData>(),
            DocumentType.Xml => new XmlSerializer<TData>(),
            _ => throw new NotImplementedException("Not supported type")
        };
    }
}

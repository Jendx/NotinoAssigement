namespace Notino.Domain.Serializers;

using Notino.Domain.Models.Abstraction;
using Notino.Domain.Serializers.Abstraction;
using System.Xml.Serialization;

internal sealed class XmlSerializer<TData> : ISerializer<TData>
    where TData : IModel
{
    private readonly XmlSerializer _serializer = new XmlSerializer(typeof(TData));

    public string Serialize(TData data)
    {
        // Serialize the object to an XML string
        using var stringWriter = new StringWriter();

        _serializer.Serialize(stringWriter, data);
        return stringWriter.ToString();
    }
}

namespace Notino.Domain.Models;

using Notino.Domain.Models.Abstraction;

public sealed class Document : IModel
{
    public Guid Id { get; set; }

    public List<Tags> Tags { get; set; }

    public object Data { get; set; }
}
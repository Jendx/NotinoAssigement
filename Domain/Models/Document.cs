namespace Notino.Domain.Models;

public sealed class Document
{
    public Guid Id { get; set; }

    public List<string> Tags { get; set; }

    public Data Data { get; set; }
}
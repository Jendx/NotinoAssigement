using Notino.Domain.Models.Abstraction;

namespace Notino.Data.InMemoryEF.Entities;

public class DocumentEntity : IModel
{
    public Guid Id { get; set; }

    public List<string> Tags { get; set; }

    public object Data { get; set; }
}
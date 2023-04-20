namespace Notino.Domain.Models;

using Notino.Domain.Models.Abstraction;

public sealed class Tags : IModel
{
    public Guid Id { get; set; }

    public string TagName { get; set; }
}
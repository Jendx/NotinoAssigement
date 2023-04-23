namespace Notino.Domain.Commands.DocumentCommands;

using Notino.Domain.Commands.Abstraction;

public sealed class CreateDocumentCommand : ICommand
{
    public Guid Id { get; set; }

    public List<string> Tags { get; set; }

    public object Data { get; set; }
}

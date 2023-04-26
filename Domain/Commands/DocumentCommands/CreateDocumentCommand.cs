namespace Notino.Domain.Commands.DocumentCommands;

using Notino.Domain.Commands.Abstraction;

public sealed class CreateDocumentCommand : ICommand
{
    public Guid Id { get; init; }

    public List<string> Tags { get; init; }

    public object Data { get; init; }
}

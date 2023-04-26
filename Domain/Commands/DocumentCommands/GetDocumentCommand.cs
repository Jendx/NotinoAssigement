namespace Notino.Domain.Commands.DocumentCommands;

using Notino.Domain.Commands.Abstraction;

public sealed class GetDocumentCommand : ICommand
{
    public required Guid Id { get; init; }
}

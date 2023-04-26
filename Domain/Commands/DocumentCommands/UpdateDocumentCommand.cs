namespace Notino.Domain.Commands.DocumentCommands;

using Notino.Domain.Commands.Abstraction;
using Notino.Domain.Commands.TagCommands;

public sealed class UpdateDocumentCommand : ICommand
{
    public Guid Id { get; init; }

    public List<UpdateTagCommand> Tags { get; init; }

    public object Data { get; init; }
}

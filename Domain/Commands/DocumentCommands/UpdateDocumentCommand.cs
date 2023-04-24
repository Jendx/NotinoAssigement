namespace Notino.Domain.Commands.DocumentCommands;

using Notino.Domain.Commands.Abstraction;
using Notino.Domain.Commands.TagCommands;

public sealed class UpdateDocumentCommand : ICommand
{
    public Guid Id { get; set; }

    public List<UpdateTagCommand> Tags { get; set; }

    public object Data { get; set; }
}

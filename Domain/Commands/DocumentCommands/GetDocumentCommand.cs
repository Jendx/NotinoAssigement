using Notino.Domain.Commands.Abstraction;
using Notino.Domain.Enums;

namespace Notino.Domain.Commands.DocumentCommands;

public sealed class GetDocumentCommand : ICommand
{
    public required Guid Id { get; set; }
}

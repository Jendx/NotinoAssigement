using Notino.Domain.Commands.Abstraction;

namespace Notino.Domain.Commands.TagCommands;

public sealed class UpdateTagCommand : ICommand
{
    public Guid Id { get; set; }

    public string Tag { get; set; }
}

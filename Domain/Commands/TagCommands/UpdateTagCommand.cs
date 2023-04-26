namespace Notino.Domain.Commands.TagCommands;

using Notino.Domain.Commands.Abstraction;

public sealed class UpdateTagCommand : ICommand
{
    public Guid Id { get; init; }

    public string Tag { get; init; }
}

namespace Notino.Domain.Commands.Abstraction;

public interface ICommand
{
    public Guid Id { get; init; }
}

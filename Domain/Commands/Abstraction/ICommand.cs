namespace Notino.Domain.Commands.Abstraction;

using Notino.Domain.Models.Abstraction;

public interface ICommand
{
    public Guid Id { get; set; }
}

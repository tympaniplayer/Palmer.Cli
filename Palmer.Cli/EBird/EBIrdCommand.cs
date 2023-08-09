using MediatR;

namespace Palmer.Cli.EBird;

public sealed class EBirdCommand : IEBirdCommand
{
    private readonly IMediator _mediator;

    public EBirdCommand(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public Task NotableBirds(IConsole console)
    {
        throw new NotImplementedException();
    }
}
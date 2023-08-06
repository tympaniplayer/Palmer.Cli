using CSharpFunctionalExtensions;
using MediatR;

namespace Palmer.Cli.ExternalProviders.EBird.NotableObservations;

public record Request() : IRequest<Result<Response>>;
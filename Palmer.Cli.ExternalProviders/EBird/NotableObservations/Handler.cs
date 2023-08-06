using CSharpFunctionalExtensions;
using MediatR;

namespace Palmer.Cli.ExternalProviders.EBird.NotableObservations;

public sealed class Handler : IRequestHandler<Request, Result<Response>>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public Handler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        
    }
}
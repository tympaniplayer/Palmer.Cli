using System.Text.Json;
using CSharpFunctionalExtensions;
using MediatR;
using Palmer.Cli.ExternalProviders.EBird.Configuration;
using Palmer.Cli.ExternalProviders.EBird.DTOs;

namespace Palmer.Cli.ExternalProviders.EBird.NotableObservations;

public sealed class Handler : IRequestHandler<Request, Result<IEnumerable<Response>>>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public Handler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<Result<IEnumerable<Response>>> Handle(Request request, CancellationToken cancellationToken)
        => await Result.Success()
            .Map(() => _httpClientFactory.CreateClient(ClientConfiguration.EBirdHttpClient))
            .Map(client => client.GetAsync("v2/data/obs/US-GA-113/recent/notable"))
            .Ensure(response => response.IsSuccessStatusCode,
                response => $"Error calling eBird: {response.StatusCode} {response.ReasonPhrase}")
            .Map(response => response.Content.ReadAsStringAsync(cancellationToken))
            .Map(stringResponse => JsonSerializer.Deserialize<NotableResponse[]>(stringResponse))
            .EnsureNotNull("No notable observations or error reading response")
            .Map(notables => notables.Select(notable => new Response(
                    notable.ComName,
                    notable.LocName,
                    DateTime.Parse(notable.ObsDt),
                    notable.HowMany,
                    notable.ObsValid,
                    notable.ObsReviewed,
                    notable.LocationPrivate,
                    new Uri($"https://ebird.org/checklist/{notable.SubId})"))));


}
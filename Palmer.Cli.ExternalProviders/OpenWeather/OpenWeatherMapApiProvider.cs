using System.Text.Json;
using System.Text.Json.Serialization;
using Palmer.Cli.Common.Json;
using Palmer.Cli.ExternalProviders.OpenWeather.Response;

namespace Palmer.Cli.ExternalProviders.OpenWeather;

public sealed class OpenWeatherMapApiProvider : IOpenWeatherMapApiProvider
{
    private IHttpClientFactory _httpClientFactory;
    public static readonly string HttpClientName = "OpenWeatherMap";
    private readonly string? _openWeatherApiKey;

    public OpenWeatherMapApiProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            // ReSharper disable once InconsistentNaming Reason: Return "OPEN_API" in argument null exception
        _openWeatherApiKey = Environment.GetEnvironmentVariable("OPENWEATHERMAP_API_KEY");
        if (string.IsNullOrWhiteSpace(_openWeatherApiKey))
        {
            throw new Exception("OPENWEATHERMAP_API_KEY Environment Variable is not set");
        }
    }

    public async Task<GeocodeResponse> GetGeocodeResponse(string zip)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var geocodeResponse =
            await httpClient.GetAsync(
                $"geo/1.0/zip?zip={zip},US&limit=1&appid={_openWeatherApiKey}");

        if (!geocodeResponse.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Error with OpenWeatherMap Api {geocodeResponse.StatusCode}\n\t{geocodeResponse.ReasonPhrase}");
        }
        var geocodedResponseString = await geocodeResponse.Content.ReadAsStringAsync();
        var deserializedGeoResponse = JsonSerializer.Deserialize<GeocodeResponse>(
            geocodedResponseString, 
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            });
        if (deserializedGeoResponse is null)
        {
            throw new InvalidOperationException($"Error deserializing geocoded response: {geocodedResponseString}");
        }
        return deserializedGeoResponse;
    }

    public async Task<WeatherResponse> GetWeatherResponse(double lon, double lat)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        //https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API key}
        var weatherResponse =
            await httpClient.GetAsync($"data/2.5/weather?lat={lat}&lon={lon}&units=imperial&appid={_openWeatherApiKey}");
        
        if (!weatherResponse.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Error with OpenWeatherMap Api {weatherResponse.StatusCode}\n\t{weatherResponse.ReasonPhrase}");
        }

        var weatherResponseString = await weatherResponse.Content.ReadAsStringAsync();
        var deserializedWeatherResponse = JsonSerializer.Deserialize<WeatherResponse>(
            weatherResponseString,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            });

        if (deserializedWeatherResponse is null)
        {
            throw new InvalidOperationException($"Error deserializing weather response: {weatherResponseString}");
        }

        return deserializedWeatherResponse;
    }
}
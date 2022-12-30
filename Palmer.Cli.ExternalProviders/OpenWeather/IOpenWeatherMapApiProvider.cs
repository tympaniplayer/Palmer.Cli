using Palmer.Cli.ExternalProviders.OpenWeather.Response;

namespace Palmer.Cli.ExternalProviders.OpenWeather;

public interface IOpenWeatherMapApiProvider
{
    public Task<GeocodeResponse> GetGeocodeResponse(string zip);

    public Task<WeatherResponse> GetWeatherResponse(double lon, double lat);
}
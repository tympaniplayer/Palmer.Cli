using Palmer.Cli.ExternalProviders.OpenWeather;

namespace Palmer.Cli.Weather;

public sealed class WeatherCommand : IWeatherCommand
{
    private readonly IOpenWeatherMapApiProvider _openWeatherMapApiProvider;

    public WeatherCommand(IOpenWeatherMapApiProvider openWeatherMapApiProvider)
    {
        _openWeatherMapApiProvider = openWeatherMapApiProvider ??
                                     throw new ArgumentNullException(nameof(openWeatherMapApiProvider));
    }

    public async Task WeatherByZip(IConsole console, string zip)
    {
        var geocodeResponse = await _openWeatherMapApiProvider.GetGeocodeResponse(zip);
        var weatherResponse =
            await _openWeatherMapApiProvider.GetWeatherResponse(geocodeResponse.Lon, geocodeResponse.Lat);
        var weather = weatherResponse.Weather.First();
        console.WriteLine("Weather Response");
        console.WriteLine($"\tTemp: {weatherResponse.Main.Temp}ºF");
        console.WriteLine($"\tMain: {weather.Main}");
        console.WriteLine($"\tDescription: {weather.Description}");
        console.WriteLine($"\tHumidity: {weatherResponse.Main.Humidity}%");
    }
}
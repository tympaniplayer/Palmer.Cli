namespace Palmer.Cli.Weather;

public interface IWeatherCommand
{
    [Command("zip",
        Usage = "zip <zipcode>",
        Description = "Returns forecast for specific zipcode supplied.",
        ExtendedHelpText = "Requires an OpenAPI Key and environment variable \"OPENAPI_KEY\" set")]
    public Task WeatherByZip(IConsole console, string zip);
}
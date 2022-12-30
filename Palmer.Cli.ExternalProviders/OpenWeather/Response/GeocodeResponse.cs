namespace Palmer.Cli.ExternalProviders.OpenWeather.Response;

public sealed class GeocodeResponse
{
    public string Name { get; set; } = default!;
    public double Lat { get; set; }
    public double Lon { get; set; }
}
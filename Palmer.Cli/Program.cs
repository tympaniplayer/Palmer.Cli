using CommandDotNet.IoC.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Palmer.Cli.ExternalProviders.EBird.Configuration;
using Palmer.Cli.ExternalProviders.OpenWeather;
using Palmer.Cli.Weather;

namespace Palmer.Cli;

public sealed class Program
{
    private const string OpenWeatherMapBaseUri = "https://api.openweathermap.org/";
    private const string EBirdBaseUri = "https://api.ebird.org/v2/ref/";
    
    static int Main(string[] args)
    {
        try
        {
            var serviceProvider = ConfigureMicrosoftServiceProvider();
            return AppRunner.UseMicrosoftDependencyInjection(serviceProvider).Run(args);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.Error.WriteLine(ex.Message);
            return ExitCodes.Error.Result;
        }
    }

    private static AppRunner AppRunner => new AppRunner<Program>();

    [DefaultCommand]
    public void Hello(IConsole console) => console.WriteLine("Hello, Nate");

    [Subcommand(RenameAs = "weather")] 
    public IWeatherCommand Weather { get; } = default!;
    
    private static IServiceProvider ConfigureMicrosoftServiceProvider()
    {
        var ebird = Environment.GetEnvironmentVariable("ebird");
        var openapi
        if (ebird is null)
        {
            throw new InvalidOperationException("ebird environment variable must be set");
        }
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<Program>();
        serviceCollection.AddScoped<IWeatherCommand, WeatherCommand>();
        serviceCollection.AddScoped<IOpenWeatherMapApiProvider, OpenWeatherMapApiProvider>();
        serviceCollection.AddHttpClient(OpenWeatherMapApiProvider.HttpClientName, 
            client =>
            {
                client.BaseAddress = new Uri(OpenWeatherMapBaseUri);
            });
        serviceCollection.AddHttpClient(Configuration.EBirdHttpClient,
            client =>
            {
                client.BaseAddress = new Uri(EBirdBaseUri);
                client.DefaultRequestHeaders.Add("x-ebirdapitoken", ebird);
            });
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider;
    }
}
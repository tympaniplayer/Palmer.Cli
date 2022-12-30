using CommandDotNet.IoC.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Palmer.Cli.ExternalProviders.OpenWeather;
using Palmer.Cli.Weather;

namespace Palmer.Cli;

public sealed class Program
{
    private const string OpenWeatherMapBaseUri = "https://api.openweathermap.org/";
    
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
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<Program>();
        serviceCollection.AddScoped<IWeatherCommand, WeatherCommand>();
        serviceCollection.AddScoped<IOpenWeatherMapApiProvider, OpenWeatherMapApiProvider>();
        serviceCollection.AddHttpClient(OpenWeatherMapApiProvider.HttpClientName, client =>
        {
            client.BaseAddress = new Uri(OpenWeatherMapBaseUri);
        });
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider;
    }
}
namespace Palmer.Cli.EBird;

public interface IEBirdCommand
{
    [Command("birds",
        Usage = "birds",
        Description = "Returns notable bird observations from ebird for Fayette County GA",
        ExtendedHelpText = "Requires an eBird key and environment variable \"bird\" set")]
    public Task NotableBirds(IConsole console);
}
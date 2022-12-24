
public sealed class Program
{
    static int Main(string[] args) => AppRunner.Run(args);

    public static AppRunner AppRunner => new AppRunner<Program>();

    [DefaultCommand]
    public void Hello(IConsole console) => console.WriteLine("Hello, Nate");
}
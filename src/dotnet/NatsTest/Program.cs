namespace NatsTest;

public class Program
{

    public static void Main(string[] args)
    {
        var app = HostBuilderHelper.CreateApp(args);

        app.Run();
    }
}

namespace NatsTest;

using Microsoft.Extensions.Configuration;

public static class HostBuilderHelper
{
    public static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddNatsConfiguration()
            .AddEnvironmentVariables();

        builder.Services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddNatsTest(builder.Configuration)
            .AddControllers();

        builder.Logging.ClearProviders()
            .AddSimpleConsole(o =>
                     {
                         o.IncludeScopes = true;
                         o.SingleLine = true;
                         o.TimestampFormat = "yyyy/MM/dd HH:mm:ss ";
                     });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
        }
        return app;
    }
}

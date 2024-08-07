namespace NatsTest;

using Microsoft.Extensions.Configuration;

public static class HostBuilderHelper
{
    public static WebApplication CreateApp(string[] args)
    {
        var config = new ConfigurationBuilder()
         .AddNatsConfiguration()
         .AddEnvironmentVariables()
         .Build();


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddConfig(config)
            .AddNatsTest()
            .AddControllers();

        builder.Logging.ClearProviders()
            .AddSimpleConsole(o =>
                     {
                         o.IncludeScopes = true;
                         o.SingleLine = true;
                         o.TimestampFormat = "yyyy/MM/dd HH:mm:ss ";
                     });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
        }
        return app;
    }
}

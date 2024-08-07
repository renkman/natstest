namespace NatsTest;

using Microsoft.Extensions.Configuration;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = new ConfigurationBuilder()
         .AddNatsConfiguration()
         .AddEnvironmentVariables()
         .Build();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddConfig(config)
            .AddNatsTest()
            .AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
        }

        app.Run();
    }
}

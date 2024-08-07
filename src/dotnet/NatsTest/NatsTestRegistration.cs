namespace NatsTest;

using Microsoft.Extensions.DependencyInjection.Extensions;
using NatsTest.Services;
using NatsTest.config;

public static class NatsRegistration
{
    public static IServiceCollection AddNatsTest(this IServiceCollection services)
    {
        services.TryAddSingleton<IMessageService, MessageService>();

        return services;
    }

    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NatsConfig>(configuration.GetSection("Nats"));

        services.TryAddSingleton(configuration.GetSection("Nats").Get<NatsConfig>()!);

        return services;
    }

    public static ConfigurationBuilder AddNatsConfiguration(this ConfigurationBuilder configBuilder)
    {
        configBuilder.AddJsonFile("config/nats.json");
        return configBuilder;
    }
}

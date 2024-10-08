namespace NatsTest;

using Microsoft.Extensions.DependencyInjection.Extensions;
using NatsTest.Services;
using NatsTest.Nats;
using NatsTest.config;

public static class NatsRegistration
{

    public static IServiceCollection AddNatsTest(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton(configuration.GetSection("Nats").Get<NatsConfig>()!);

        services.TryAddSingleton<INatsConnectionFactory, NatsConnectionFactory>();
        services.TryAddSingleton<INatsSender, NatsSender>();
        services.TryAddSingleton<IMessageService, MessageService>();

        return services;
    }

    public static IConfigurationBuilder AddNatsConfiguration(this IConfigurationBuilder configBuilder)
    {
        configBuilder.AddJsonFile("config/nats.json");
        return configBuilder;
    }
}

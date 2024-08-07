namespace NatsTest.Nats;

using NATS.Client.Core;
using NatsTest.config;

public class NatsConnectionFactory : INatsConnectionFactory
{
    private readonly NatsConfig _natsConfig;

    public NatsConnectionFactory(NatsConfig natsConfig)
    {
        _natsConfig = natsConfig;
    }

    public NatsConnection Create()
        => new NatsConnection(new NatsOpts
        {
            Url = _natsConfig.Url
        });
}

using NATS.Client.Core;

namespace NatsTest.Nats;

public interface INatsConnectionFactory
{
    NatsConnection Create();
}

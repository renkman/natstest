using NATS.Client.Core;

namespace NatsTest.Nats;

public class NatsSender : INatsSender, IAsyncDisposable
{
    private readonly INatsConnection _nats;

    private readonly ILogger<NatsSender> _logger;

    public NatsSender(INatsConnectionFactory natsConnectionFactory, ILogger<NatsSender> logger)
    {
        _nats = natsConnectionFactory.Create();
        _logger = logger;
    }

    public async Task PublishAsync(string subject, string message)
    {
        _logger.LogInformation("Send {Message} on subject {Subject}", message, subject);
        await _nats.PublishAsync(subject, message);
    }

    public async ValueTask DisposeAsync()
    {
        await _nats.DisposeAsync();
    }
}

namespace NatsTest.Services;

using System.Threading.Tasks;
using NATS.Client.Core;
using NatsTest.config;

public class MessageService : IMessageService
{
    const string NatsSubject = "message";

    INatsConnection _nats;

    public MessageService(NatsConfig natsConfig)
    {
        _nats = new NatsConnection(new NatsOpts { Url = natsConfig.Url });
    }

    public async Task SendMessage(string message)
    {
        await _nats.PublishAsync(NatsSubject, message);
    }
}

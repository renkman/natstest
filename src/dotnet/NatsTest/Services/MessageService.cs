namespace NatsTest.Services;

using System.Threading.Tasks;
using NatsTest.Nats;

public class MessageService : IMessageService
{
    const string NatsSubject = "message";

    INatsSender _natsSender;

    public MessageService(INatsSender natsSender)
    {
        _natsSender = natsSender;
    }

    public async Task SendMessage(string message)
        => await _natsSender.PublishAsync(NatsSubject, message);
}

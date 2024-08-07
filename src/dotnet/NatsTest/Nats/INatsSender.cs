namespace NatsTest.Nats;

public interface INatsSender
{
    Task PublishAsync(string subject, string message);
}

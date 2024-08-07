namespace NatsTest.Services;

public interface IMessageService
{
    Task SendMessage(string message);
}

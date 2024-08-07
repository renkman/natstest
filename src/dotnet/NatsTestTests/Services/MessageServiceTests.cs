namespace NatsTestTests;

using Microsoft.Extensions.Logging;
using NatsTest.Services;
using NatsTest.Nats;
using Moq;

public class MessageServiceTests
{
    private readonly Mock<INatsSender> _natsSenderMock = new(MockBehavior.Strict);

    [Fact]
    public async Task Send_WithMessage_CallsNatsSend()
    {
        const string message = "Foobar";

        _natsSenderMock.Setup(n => n.PublishAsync("message", message)).Returns(Task.CompletedTask);

        var service = new MessageService(_natsSenderMock.Object);

        await service.SendMessage(message);

        _natsSenderMock.Verify(n => n.PublishAsync("message", message), Times.Once);
    }
}

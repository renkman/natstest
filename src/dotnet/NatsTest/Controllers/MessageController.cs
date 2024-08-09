using Microsoft.AspNetCore.Mvc;
using NatsTest.Services;

namespace NatsTest.Controllers;

[ApiController]
[Route("api/message")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    [Route("send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SendMessage([FromBody] string message)
    {
        await _messageService.SendMessage(message);
        return Ok();
    }
}

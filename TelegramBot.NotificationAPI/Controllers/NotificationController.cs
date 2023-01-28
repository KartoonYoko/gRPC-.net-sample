using Microsoft.AspNetCore.Mvc;

namespace TelegramBot.NotificationAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : Controller
{
    private readonly IUpdateHandler _updateHandler;
    public NotificationController(IUpdateHandler updateHandler) {
        _updateHandler = updateHandler;
    }

    public async Task<IActionResult> Handle(string token, [FromBody] Update update) {
        if (update.Message != null) 
            await _updateHandler.MessageAsync(token, update.Message);
        else if (update.EditedMessage != null) { }
        else if (update.EditedChannelPost != null) { }

        return Ok();
    }
}



namespace Notifications.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TelegramController : Controller
{

    [HttpPost("{token}")]
    public IActionResult HandleNotification(string token, [FromBody] Update update) {
        // TODO redirect to telegram service
        return Ok();
    }

}

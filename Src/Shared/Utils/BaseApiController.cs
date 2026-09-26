using Microsoft.AspNetCore.Mvc;

namespace access_manager_api.Shared.Utils;

public class BaseApiController : ControllerBase
{
    protected IActionResult FromResponse<T>(ApiResponse<T> response)
    {
        return StatusCode(response.StatusCode, response);
    }
}
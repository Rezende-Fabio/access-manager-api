using access_manager_api.Api.Dtos.SetupDtos;
using access_manager_api.Application.Interfaces.SetupInt;
using access_manager_api.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace access_manager_api.Api.Controllers;

[ApiController]
[Route("api/v1/setup")]
[AllowAnonymous]
public class SetupController : BaseApiController
{
    private readonly ISetupService _setupService;

    public SetupController(ISetupService setupService)
    {
        _setupService = setupService;
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        var isInitialized = await _setupService.IsSystemInitializedAsync();

        return FromResponse(ApiResponse<SetupStatusDto>.SuccessResponse(
            new SetupStatusDto { IsInitialized = isInitialized }));
    }


    [HttpPost("initialize")]
    public async Task<IActionResult> Initialize([FromBody] InitializeSystemRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return FromResponse(ApiResponse<InitializeSystemResponseDto>.ErrorResponse(
                "Dados inválidos", errors, 400));
        }

        var result = await _setupService.InitializeSystemAsync(request);

        return FromResponse(ApiResponse<InitializeSystemResponseDto>.SuccessResponse(
            result, "Sistema inicializado com sucesso", 201));
    }
}
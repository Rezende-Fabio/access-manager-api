using access_manager_api.Api.Dtos.SetupDtos;

namespace access_manager_api.Application.Interfaces.SetupInt;

public interface ISetupService
{
    Task<bool> IsSystemInitializedAsync();
    Task<InitializeSystemResponseDto> InitializeSystemAsync(InitializeSystemRequestDto request);
}

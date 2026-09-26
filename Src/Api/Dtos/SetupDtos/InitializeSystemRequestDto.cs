namespace access_manager_api.Api.Dtos.SetupDtos;

public class InitializeSystemRequestDto
{
    public string BranchName { get; set; } = string.Empty;
    public string BranchCnpj { get; set; } = string.Empty;

    public string AdminName { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
}
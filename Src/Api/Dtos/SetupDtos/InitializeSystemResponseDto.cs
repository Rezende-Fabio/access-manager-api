namespace access_manager_api.Api.Dtos.SetupDtos;

public class InitializeSystemResponseDto
{
    public string AdminEmail { get; set; } = string.Empty;
    public string TemporaryPassword { get; set; } = string.Empty;
    public string Message { get; set; } =
        "Guarde essa senha agora — ela não será mostrada novamente. " +
        "Você precisará trocá-la no primeiro login.";
}
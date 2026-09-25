namespace access_manager_api.Domain.Models;

public class MenuPermission
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MenuId { get; set; }
    public Menu Menu { get; set; } = null!;

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}
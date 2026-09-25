namespace access_manager_api.Domain.Models;

public class Permission : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Guid? ModuleId { get; set; }
    public Module? Module { get; set; }

    public ICollection<ProfilePermission> ProfilePermissions { get; set; } = new List<ProfilePermission>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<MenuPermission> MenuPermissions { get; set; } = new List<MenuPermission>();
}
namespace access_manager_api.Domain.Models;

public class Menu : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Route { get; set; }
    public int Order { get; set; }

    public Guid? ParentMenuId { get; set; }
    public Menu? ParentMenu { get; set; }
    public ICollection<Menu> Children { get; set; } = new List<Menu>();

    public ICollection<MenuPermission> MenuPermissions { get; set; } = new List<MenuPermission>();
}
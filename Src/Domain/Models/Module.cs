namespace access_manager_api.Domain.Models;

public class Module : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
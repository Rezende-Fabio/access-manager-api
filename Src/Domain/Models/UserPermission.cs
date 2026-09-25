using access_manager_api.Domain.Enums;

namespace access_manager_api.Domain.Models;

public class UserPermission
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;

    public Guid? BranchId { get; set; }       // null = vale em todas as filiais
    public Branch? Branch { get; set; }

    public PermissionOverrideType Type { get; set; }
}
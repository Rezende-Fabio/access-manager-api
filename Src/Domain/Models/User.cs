namespace access_manager_api.Domain.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public bool MustChangePassword { get; set; } = false;
    public DateTime? PasswordChangedAt { get; set; } 

    public Guid? DefaultBranchId { get; set; }
    public Branch? DefaultBranch { get; set; }

    public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
    public ICollection<UserProfileBranch> UserProfileBranches { get; set; } = new List<UserProfileBranch>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
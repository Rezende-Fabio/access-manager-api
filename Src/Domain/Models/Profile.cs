namespace access_manager_api.Domain.Models;

public class Profile : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<UserProfileBranch> UserProfileBranches { get; set; } = new List<UserProfileBranch>();
    public ICollection<ProfilePermission> ProfilePermissions { get; set; } = new List<ProfilePermission>();
}
namespace access_manager_api.Domain.Models;

public class Branch : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
    public ICollection<UserProfileBranch> UserProfileBranches { get; set; } = new List<UserProfileBranch>();
}
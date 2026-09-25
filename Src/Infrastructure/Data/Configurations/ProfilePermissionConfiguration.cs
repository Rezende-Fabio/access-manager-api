using access_manager_api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace access_manager_api.Infrastructure.Data.Configurations;

public class ProfilePermissionConfiguration : IEntityTypeConfiguration<ProfilePermission>
{
    public void Configure(EntityTypeBuilder<ProfilePermission> builder)
    {
        builder.ToTable("ProfilePermissions");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.ProfileId, x.PermissionId })
            .IsUnique();
    }
}
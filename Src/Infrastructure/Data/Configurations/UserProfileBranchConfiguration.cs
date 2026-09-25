using access_manager_api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace access_manager_api.Infrastructure.Data.Configurations;

public class UserProfileBranchConfiguration : IEntityTypeConfiguration<UserProfileBranch>
{
    public void Configure(EntityTypeBuilder<UserProfileBranch> builder)
    {
        builder.ToTable("UserProfileBranches");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.BranchId })
            .IsUnique();

        builder.HasOne(x => x.Profile)
            .WithMany(p => p.UserProfileBranches)
            .HasForeignKey(x => x.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Branch)
            .WithMany(b => b.UserProfileBranches)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
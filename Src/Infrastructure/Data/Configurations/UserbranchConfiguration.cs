using access_manager_api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace access_manager_api.Infrastructure.Data.Configurations;

public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
{
    public void Configure(EntityTypeBuilder<UserBranch> builder)
    {
        builder.ToTable("UserBranches");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.BranchId })
            .IsUnique();

        builder.HasOne(x => x.Branch)
            .WithMany(b => b.UserBranches)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
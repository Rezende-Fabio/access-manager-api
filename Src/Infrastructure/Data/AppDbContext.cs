using access_manager_api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace access_manager_api.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<UserBranch> UserBranches { get; set; }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<UserProfileBranch> UserProfileBranches { get; set; }

    public DbSet<Module> Modules { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<ProfilePermission> ProfilePermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuPermission> MenuPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}

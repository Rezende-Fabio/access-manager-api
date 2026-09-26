using access_manager_api.Api.Dtos.SetupDtos;
using access_manager_api.Application.Interfaces.RepositoryInt;
using access_manager_api.Application.Interfaces.SetupInt;
using access_manager_api.Application.Interfaces.UnitOfWorkInt;
using access_manager_api.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace access_manager_api.Application.Services;

public class SetupService : ISetupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Profile> _profileRepository;
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IRepository<ProfilePermission> _profilePermissionRepository;
    private readonly IRepository<UserBranch> _userBranchRepository;
    private readonly IRepository<UserProfileBranch> _userProfileBranchRepository;

    private readonly PasswordHasher<User> _passwordHasher = new();

    private static readonly (string Code, string Description)[] BasePermissions =
    {
        ("users.view",       "View users"),
        ("users.create",     "Create users"),
        ("users.edit",       "Edit users"),
        ("users.delete",     "Delete users"),

        ("branches.view",    "View branches"),
        ("branches.create",  "Create branches"),
        ("branches.edit",    "Edit branches"),

        ("profiles.view",    "View profiles"),
        ("profiles.create",  "Create profiles"),
        ("profiles.edit",    "Edit profiles"),

        ("permissions.view", "View permissions"),

        ("menus.view",       "View menus"),
        ("menus.edit",       "Edit menus"),
    };

    public SetupService(
        IUnitOfWork unitOfWork,
        IRepository<User> userRepository,
        IRepository<Branch> branchRepository,
        IRepository<Profile> profileRepository,
        IRepository<Permission> permissionRepository,
        IRepository<ProfilePermission> profilePermissionRepository,
        IRepository<UserBranch> userBranchRepository,
        IRepository<UserProfileBranch> userProfileBranchRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _branchRepository = branchRepository;
        _profileRepository = profileRepository;
        _permissionRepository = permissionRepository;
        _profilePermissionRepository = profilePermissionRepository;
        _userBranchRepository = userBranchRepository;
        _userProfileBranchRepository = userProfileBranchRepository;
    }

    public async Task<bool> IsSystemInitializedAsync()
    {
        return await _userRepository.AnyAsync();
    }

    public async Task<InitializeSystemResponseDto> InitializeSystemAsync(InitializeSystemRequestDto request)
    {
        if (await IsSystemInitializedAsync())
        {
            throw new InvalidOperationException(
                "O sistema já foi inicializado. O setup só pode ser executado uma vez.");
        }

        return await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var branch = new Branch
            {
                Name = request.BranchName,
                Cnpj = request.BranchCnpj
            };
            await _branchRepository.AddAsync(branch);

            var permissions = BasePermissions
                .Select(p => new Permission { Code = p.Code, Description = p.Description })
                .ToList();
            _permissionRepository.AddRange(permissions);

            var adminProfile = new Profile
            {
                Name = "Administrator",
                Description = "Full access profile created automatically during system setup."
            };
            await _profileRepository.AddAsync(adminProfile);

            var profilePermissions = permissions
                .Select(p => new ProfilePermission { Profile = adminProfile, Permission = p })
                .ToList();
            _profilePermissionRepository.AddRange(profilePermissions);

            var temporaryPassword = GenerateRandomPassword();

            var adminUser = new User
            {
                Name = request.AdminName,
                Email = request.AdminEmail,
                MustChangePassword = true,
                DefaultBranch = branch
            };
            adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, temporaryPassword);
            await _userRepository.AddAsync(adminUser);

            await _userBranchRepository.AddAsync(new UserBranch
            {
                User = adminUser,
                Branch = branch
            });

            await _userProfileBranchRepository.AddAsync(new UserProfileBranch
            {
                User = adminUser,
                Profile = adminProfile,
                Branch = branch
            });

            return new InitializeSystemResponseDto
            {
                AdminEmail = adminUser.Email,
                TemporaryPassword = temporaryPassword
            };
        });
    }

    private static string GenerateRandomPassword(int length = 12)
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$";
        var random = Random.Shared;

        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[random.Next(chars.Length)])
            .ToArray());
    }
}
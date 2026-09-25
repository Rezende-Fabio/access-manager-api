using Microsoft.AspNetCore.Authorization;

namespace access_manager_api.Shared.Auth.Requirements;

public class SameUserRequirement : IAuthorizationRequirement { }
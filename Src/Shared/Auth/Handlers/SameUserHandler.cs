using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using access_manager_api.Shared.Auth.Requirements;

namespace access_manager_api.Shared.Auth.Handlers;

public class SameUserHandler : AuthorizationHandler<SameUserRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SameUserHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SameUserRequirement requirement
    )
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var userIdToken))
            return Task.CompletedTask;

        var routeValue = _httpContextAccessor
            .HttpContext?.Request.RouteValues["idUser"]
            ?.ToString();

        if (!Guid.TryParse(routeValue, out var routeId))
            return Task.CompletedTask;

        if (userIdToken == routeId)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
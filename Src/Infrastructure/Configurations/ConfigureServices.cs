
namespace access_manager_api.Infrastructure.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddHttpContextAccessor();

        services.AddCors(options =>
        {
            options.AddPolicy(
                name: "AllowAll",
                policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                }
            );
        });

        return services;
    }
}
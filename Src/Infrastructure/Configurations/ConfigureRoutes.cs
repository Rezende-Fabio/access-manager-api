
namespace access_manager_api.Infrastructure.Configurations;

public static class ConfigureRoutes
{
    public static void UseAppRoutes(this WebApplication app)
    {
        app.MapControllers();
        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
        });
    }
}
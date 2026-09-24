using access_manager_api.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos Serviços
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

// Configuração das rotas
app.UseAppRoutes();

app.Run();
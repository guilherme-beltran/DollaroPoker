using Backoffice.Application.Interfaces.Punters;
using Backoffice.Application.Interfaces.Users;
using Backoffice.Application.UseCases.Login;
using Backoffice.Application.UseCases.Punters.Create;
using Backoffice.Application.UseCases.Users.Create;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Domain.Interfaces.Repositories.Cache;
using Backoffice.Domain.Interfaces.Services;
using Backoffice.Domain.Interfaces.UnitOfWork;
using Backoffice.Infra.Contexts.Backoffice;
using Backoffice.Infra.Repositories;
using Backoffice.Infra.Repositories.Cache;
using Backoffice.Infra.Services;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.API.Extensions;

public static class ServiceExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BackofficeContext>(options => 
            options.UseMySql(configuration.GetConnectionString("Database"),
            new MySqlServerVersion(new Version(8, 0, 35))));

        services.AddScoped<IBackofficeContext, BackofficeContext>();
        
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJurisdictionRepository, JurisdictionRepository>();
        services.AddScoped<ISequenceRepository, SequenceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPunterRepository, PunterRepository>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<ICachePunterRepository, CachePunterRepository>();
        services.AddScoped<ICacheUserRepository, CacheUserRepository>();

        services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        services.AddScoped<ICreatePunterHandler, CreatePunterHandler>();
        services.AddScoped<ILoginHandler, LoginHandler>();
    }

}
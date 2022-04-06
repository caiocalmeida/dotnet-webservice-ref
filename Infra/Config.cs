using DotnetWsRef.Infra.Data;
using DotnetWsRef.Infra.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetWsRef.Infra;

public static class Config
{
    public static void RegisterDependencies(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(
            opts => opts.UseNpgsql(connectionString),
            ServiceLifetime.Singleton
        );

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IEmailAgent, FakeEmailAgent>();
    }
}
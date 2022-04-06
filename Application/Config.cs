using DotnetWsRef.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetWsRef.Application;

public static class Config
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
    }
}
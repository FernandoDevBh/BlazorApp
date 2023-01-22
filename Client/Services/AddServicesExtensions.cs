using Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Services;

public static class AddServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICategoryService, SetService>();
        return services;
    }
}

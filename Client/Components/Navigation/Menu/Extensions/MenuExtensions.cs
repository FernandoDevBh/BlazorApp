using Client.Components.Navigation.Menu.Services;
using Client.Services;
using Client.Components.Navigation.Menu.ViewModel;

namespace Client.Components.Navigation.Menu.Extensions;

public static class MenuExtensions
{
    public static IServiceCollection AddMenuServices(this IServiceCollection services)
    {
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IMenuViewModel, MenuViewModel>();
        return services;
    }
}

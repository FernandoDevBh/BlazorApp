namespace Client.Pages.Set.ViewModels;

public static class SetServices
{
    public static IServiceCollection AddCategoryServices(this IServiceCollection services)
    {
        services.AddScoped<ISetListViewModel, SetListViewModel>();
        services.AddScoped<ISetViewModel, SetViewModel>();
        return services;
    }
}

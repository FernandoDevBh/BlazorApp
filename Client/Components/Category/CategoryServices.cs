using Client.Components.Category.ViewModels;

namespace Client.Components.Category;

public static class CategoryServices
{
    public static IServiceCollection AddCategoryServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryListViewModel, CategoryListViewModel>();
        services.AddScoped<ICategoryViewModel, CategoryViewModel>();
        return services;
    }
}

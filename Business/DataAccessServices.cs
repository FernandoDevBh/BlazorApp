using DataAccess;
using DataAccess.Data;
using Business.Services;
using Business.Repository;
using Business.Services.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Business.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tangy_Business;

public static class DataAccessServices
{
    public static IServiceCollection AddDataAccessServices
        (this IServiceCollection services, ConfigurationManager configuration, bool useUI = true)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        if (useUI)
        {
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>();
        }
        else
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
        }
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
        return provider;
    }
}

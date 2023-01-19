namespace API.Extensions;

public static class CorsServices
{
    public static IServiceCollection AddCorsServices(this IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy("BlazorApp", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        return services;
    }
}

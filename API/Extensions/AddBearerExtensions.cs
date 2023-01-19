using API.Helper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Extensions;

public static class AddBearerExtensions
{
    public static IServiceCollection AddBearerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettingsSection = configuration.GetSection("APISettings");
        services.Configure<APISettings>(apiSettingsSection);
        var apiSettings = apiSettingsSection.Get<APISettings>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(builder =>
        {
            builder.RequireHttpsMetadata = false;
            builder.SaveToken = true;
            builder.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(apiSettings?.GetSecretKey()),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = apiSettings?.ValidAudience,
                ValidIssuer = apiSettings?.ValidIssuer,
            };
        });

        services.AddCors(o => o.AddPolicy("Tangy", builder =>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        return services;
    }
}

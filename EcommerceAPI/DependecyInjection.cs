using Catalog.Facade.Services;
using Catalog.Service;
using Ecommerce.Facade.Services;
using Ecommerce.Service;
using System.Threading.RateLimiting;

namespace Catalog.API;

public static class DependecyInjection
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }

    public static IServiceCollection ConfigureCookies(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
        });

        return services;
    }

    public static IServiceCollection AddFixedWindowRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(o =>
        {
            o.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            o.AddPolicy("fixed", httpContext =>
            {
                var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
                if (string.IsNullOrEmpty(ipAddress))
                {
                    // Return a default RateLimitPartition if ipAddress is null or empty
                    return RateLimitPartition.GetFixedWindowLimiter("default", _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromSeconds(10)
                    });
                }

                // Return the RateLimitPartition based on the ipAddress
                return RateLimitPartition.GetFixedWindowLimiter(ipAddress, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromSeconds(10)
                });
            });
        });

        return services;
    }
}
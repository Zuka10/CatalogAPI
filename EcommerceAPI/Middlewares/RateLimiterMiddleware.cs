using System.Threading.RateLimiting;

namespace Catalog.API.Middlewares;

public static class RateLimiterMiddleware
{
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
                    return RateLimitPartition.GetFixedWindowLimiter("default", _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromSeconds(10)
                    });
                }

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
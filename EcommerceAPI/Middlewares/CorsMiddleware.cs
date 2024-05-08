namespace Catalog.API.Middlewares;

public static class CorsMiddleware
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {

        services.AddCors(options =>
        {
            options.AddPolicy("SpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();
                });
        });

        return services;
    }
}
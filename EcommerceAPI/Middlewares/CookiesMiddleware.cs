namespace Catalog.API.Middlewares;

public static class CookiesMiddleware
{
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
}
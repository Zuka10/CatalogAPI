using Catalog.Facade.Services;
using Catalog.Service;
using Ecommerce.Facade.Services;
using Ecommerce.Service;

namespace Catalog.API;

public static class DependecyInjection
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;
using Ecommerce.Repository;
using Ecommerce.Service;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<EcommerceDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MSSQLTest")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IImageService, ImageService>();
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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCors("SpecificOrigins");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
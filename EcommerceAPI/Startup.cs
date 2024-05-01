using Catalog.Facade.Services;
using Catalog.Service;
using Ecommerce.Facade.Services;
using Ecommerce.Repository;
using Ecommerce.Service;
using Microsoft.AspNetCore.Identity;
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
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<EcommerceDbContext>()
                .AddDefaultTokenProviders();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddTransient<IEmailService, EmailService>();
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
        });
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
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog API", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("SpecificOrigins");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
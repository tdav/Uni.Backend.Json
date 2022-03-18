using App.Database;
using App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions
{
    public static class MyDbContextService
    {
        public static void AddMyDbContext(this IServiceCollection services, IConfiguration conf)
        {
            services
                 .AddDbContext<MyDbContext>(opt => opt.UseNpgsql(conf.GetConnectionString("DefaultConnection"),
                                            ass => ass.MigrationsAssembly(typeof(MyDbContext).Assembly.FullName)));        

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataService, DataService>();
        }


        public static void UpdateMigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                        .GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MyDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}

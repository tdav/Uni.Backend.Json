using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions
{
    public static class MyService
    {
        public static void AddMyService(this IServiceCollection services, IConfiguration conf)
        {
            //services.AddSingleton<ISender, EmailSender>();
            //services.AddSingleton<ISender, SmsSender>();

            //services.AddScoped<ISerderService, SerderService>();
        }
    }
}

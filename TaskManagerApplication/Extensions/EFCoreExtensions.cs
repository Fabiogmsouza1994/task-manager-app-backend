using AvanadeTaskManagerApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AvanadeTaskManagerApplication.Extensions
{
    public static class EFCoreExtensions
    {
        public static IServiceCollection InjectUserDbContext(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DevConnection")));

            return services;
        }

        public static IServiceCollection InjectTaskManagerDbContext(
           this IServiceCollection services,
           IConfiguration config)
        {
            services.AddDbContext<TaskManagerTasksContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DevConnection")));

            return services;
        }
    }
}

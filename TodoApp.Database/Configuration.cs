using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.Database
{
    public static class Configuration
    {
        public static void ConfigureDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:SQL"];
            services.AddDbContext<ToDoAppContext>(options => options.UseSqlServer());
        }
    }
}
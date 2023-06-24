using Microsoft.EntityFrameworkCore;
using TodoApp.Database;

namespace TodoApp.WebApp
{
    public class UpdateDatabase
    {
        private static async Task MigrateDatabase(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoAppContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionStrings:SQL"));

            // There is no DI at current step, so need to create context manually
            await using var dbContext = new ToDoAppContext(optionsBuilder.Options);
            await dbContext.Database.MigrateAsync();

            return;
        }
    }
}
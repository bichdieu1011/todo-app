using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TodoApp.Database;

namespace TodoApp.Tools
{
    public class TodoAppContextDesignFactory : IDesignTimeDbContextFactory<ToDoAppContext>
    {
        public ToDoAppContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var configuration = configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
            Console.WriteLine("mode: " + configuration["mode"]);

            if (configuration["mode"] == "Production")
            {
                var credential = new ClientSecretCredential(configuration["TenantId"], configuration["ClientId"], configuration["ClientSecret"]);
                configurationBuilder.AddAzureKeyVault(new Uri($"https://{configuration["KeyVaultName"]}.vault.azure.net/"), credential);
                configuration = configurationBuilder.Build();
            }

            var connectionString = configuration["ConnectionString:SQL"];
            Console.WriteLine("Connection string: " + connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<ToDoAppContext>();
            optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction:
                        o => o.MigrationsAssembly(typeof(ToDoAppContext).Assembly.ToString()));
            return new ToDoAppContext(optionsBuilder.Options);
        }
    }
}
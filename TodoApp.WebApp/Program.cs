using Azure.Identity;
using Microsoft.AspNetCore;
using TodoApp.Database;
using TodoApp.WebApp;

try
{
    var host = BuildWebHost(args);
    host.MigrateDbContext<ToDoAppContext>((context, services) =>
    {
        //add custom seed data here
    });

    host.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Program exception: " + ex.ToString());
}

static IWebHost BuildWebHost(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((builderContext, config) =>
           {
               var configuration = config.Build();
               if (builderContext.HostingEnvironment.IsProduction())
               {
                   var credential = new ClientSecretCredential(configuration["AppSettings:Secret:TenantId"], configuration["AppSettings:Secret:ClientId"], configuration["AppSettings:Secret:ClientSecret"]);
                   config.AddAzureKeyVault(new Uri($"https://{configuration["AppSettings:Secret:KeyVaultName"]}.vault.azure.net/"), credential);
               }
           })
            .UseStartup<Startup>()
           .Build();
}
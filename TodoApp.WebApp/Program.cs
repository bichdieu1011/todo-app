using Azure.Identity;
using Microsoft.AspNetCore;
using TodoApp.Database;
using TodoApp.WebApp;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var isProduction = true;// builder.Environment.IsProduction();
//var configuration = GetConfiguration(isProduction);
//builder.Services.ConfigureDatabase(isProduction, configuration);
//builder.Services.ConfigureService(configuration);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularOrigins",
//        b =>
//        {
//            b.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
//        });
//});

//builder.Services.AddControllers();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseCors("AllowAngularOrigins");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

//static IConfiguration GetConfiguration(bool isProduction)
//{
//    var builder = new ConfigurationBuilder()
//        .SetBasePath(Directory.GetCurrentDirectory())
//        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//        .AddEnvironmentVariables();

//    var config = builder.Build();
//    if (isProduction)
//    {
//        var credential = new ClientSecretCredential(config["Secret:TenantId"], config["Secret:ClientId"], config["Secret:ClientSecret"]);
//        builder.AddAzureKeyVault(new Uri($"https://{config["Secret:KeyVaultName"]}.vault.azure.net/"), credential);
//    }
//    return builder.Build();
//}
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
    Console.WriteLine("Program exception: "+ ex.ToString());
}

static IWebHost BuildWebHost(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((builderContext, config) =>
           {
               var configuration = config.Build();
               //if (builderContext.HostingEnvironment.IsProduction())
               //if (true)
               {
                   var credential = new ClientSecretCredential(configuration["Secret:TenantId"], configuration["Secret:ClientId"], configuration["Secret:ClientSecret"]);
                   config.AddAzureKeyVault(new Uri($"https://{configuration["Secret:KeyVaultName"]}.vault.azure.net/"), credential);
               }
           })
            .UseStartup<Startup>()
           .Build();
}
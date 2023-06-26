using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using TodoApp.Database;
using TodoApp.Services;
using TodoApp.WebApp.Identity;
using TodoApp.WebApp.Middleware;

namespace TodoApp.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDbContext<ToDoAppContext>((s, o) =>
            {
                //var logger = s.GetRequiredService<ILogger<Startup>>();
                var connectionString = Configuration["ConnectionString:SQL"];
                //logger.LogInformation("connections tring: " + connectionString);
                o.UseSqlServer(connectionString);
            })
            .AddDbContextFactory<ToDoAppContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration["ConnectionString:SQL"],
                    sqlServerOptionsAction:
                        o => o.MigrationsAssembly(typeof(ToDoAppContext).Assembly.ToString()));
            });
            services.AddTransient<ExceptionMiddleware>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.ConfigureService(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins",
                    b =>
                    {
                        b.WithOrigins(Configuration["AllowAngularOrigins"])
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin();
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration);

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoAppApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoAppAPI v1"));
            app.UseHsts();
            if (env.IsDevelopment())
            {
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAngularOrigins");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllers();
            });
        }
    }
}
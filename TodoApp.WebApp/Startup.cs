using Microsoft.EntityFrameworkCore;
using TodoApp.Database;
using TodoApp.Services;

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
            //services.ConfigureDatabase(Configuration);
            services
                .AddDbContext<ToDoAppContext>(s =>
            {
                var connectionString = Configuration["ConnectionString:SQL"];
                s.UseSqlServer(connectionString);
            })
            .AddDbContextFactory<ToDoAppContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration["ConnectionString:SQL"],
                    sqlServerOptionsAction:
                        o => o.MigrationsAssembly(typeof(ToDoAppContext).Assembly.ToString()));
            });
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

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAngularOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllers();
            });
        }
    }
}
using System.Reflection;
using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.HitRepositories;
using HitTrackerAPI.Repositories.RunRepositories;
using HitTrackerAPI.Repositories.SplitRepositories;
using Microsoft.OpenApi.Models;

namespace HitTrackerAPI
{
    public class Configurator(IServiceCollection services)
    {
        public void BuildServices()
        {
            services.AddControllers();

            //Add policy
            services.AddCors(options => options.AddPolicy("CorsPolicy", policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            //Swagger for debugging
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HitTracker",
                    Version = "v1",
                    Description = "An API to track hits across different runs"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Scoping repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRunRepository, RunRepository>();
            services.AddScoped<ISplitRepository, SplitRepository>();
            services.AddScoped<IHitRepository, HitRepository>();

            //Setting logging level
            services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);
            });
        }

        public static void ConfigureApp(WebApplication app)
        {
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "HitTracker V1"); });

            //API
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.MapControllers();
        }
    }
}
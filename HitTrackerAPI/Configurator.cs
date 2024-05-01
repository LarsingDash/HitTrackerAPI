using HitTrackerAPI.Repositories;

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
            services.AddSwaggerGen();

            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            });
        }

        public static void ConfigureApp(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.MapControllers();
        }
    }
}
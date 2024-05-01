using HitTrackerAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Configure
        var configurator = new Configurator(builder.Services);
        configurator.BuildServices();

        //Db Context
        builder.Services.AddDbContext<HitTrackerContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("hit_tracker");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        //Build
        var app = builder.Build();

        //Seeding database
        SeedDb.SeedDatabase(app);

        //Final config and run
        Configurator.ConfigureApp(app);
        app.Run();
    }
}
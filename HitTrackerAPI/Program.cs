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
            options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("hit_tracker") ??
                                        throw new Exception("Could not not find connection string"));
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
using HitTrackerAPI.Controllers;
using HitTrackerAPI.Database;
using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.HitRepositories;
using HitTrackerAPI.Repositories.SplitRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerTest;

public class HitControllerTests
{
    private HitRepository _hitRepository = null!;
    private SplitRepository _splitRepository = null!;

    private HitController _hitController = null!;

    private readonly DbContextOptions<HitTrackerContext> _options =
        new DbContextOptionsBuilder<HitTrackerContext>().UseInMemoryDatabase("HitTracker").Options;
    private HitTrackerContext _context = null!;
    
    [SetUp]
    public void Setup()
    {
        _context = new HitTrackerContext(_options);

        _splitRepository = new SplitRepository(_context);
        _hitRepository = new HitRepository(_context);

        _hitController = new HitController(_hitRepository, _splitRepository);

        SeedDb.SeedingRun(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
    //--------------- Create Hit ---------------
    /// <summary>
    /// Tries to create a hit on splitId 3 "Gyoubu"
    /// Should work and return a new hit amount of 2
    /// </summary>
    [Test]
    public async Task CreateHit()
    {
        //Create hit
        var result = await _hitController.CreateHit(3, "Test hit");
        TestsHelper.SafetyChecks(result);
        
        //Check new hit amount
        Assert.That((result as OkObjectResult)!.Value, Is.EqualTo(2));
    }
}
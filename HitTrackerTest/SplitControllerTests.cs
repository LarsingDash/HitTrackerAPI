using System.Text.Json;
using HitTrackerAPI.Controllers;
using HitTrackerAPI.Database;
using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.SplitRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerTest;

public class SplitControllerTests
{
    private AccountRepository _accountRepository = null!;
    private SplitRepository _splitRepository = null!;

    private SplitController _splitController = null!;

    private HitTrackerContext _context = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HitTrackerContext>().UseInMemoryDatabase("HitTracker").Options;
        _context = new HitTrackerContext(options);

        _accountRepository = new AccountRepository(_context);
        _splitRepository = new SplitRepository(_context);

        _splitController = new SplitController(_accountRepository, _splitRepository);

        SeedDb.SeedingRun(_context);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    //--------------- Create Split  ---------------
    /// <summary>
    /// Happy
    ///     Tries to create the split "Bull" on Sekiro on account 0
    ///     This should create a split with id 4
    /// Duplicate
    ///     Tries to create Ogre split on an account that already has it
    ///     This should return with an error message indicating such
    /// Different
    ///     Tries to create an "Ogre" a run, while another run already has it
    ///     This should still be allowed
    /// </summary>
    [Test]
    public async Task CreateSplit()
    {
        // ----- HAPPY
        //Create "Bull" split in Sekiro
        var happy = await _splitController.CreateSplit(0, 2, "Bull");
        TestsHelper.SafetyChecks(happy);

        //Check result of CreateSplit
        Assert.That((happy as OkObjectResult)!.Value, Is.EqualTo(4));
        
        // ----- Duplicate
        //Create "Bull" split in Sekiro
        var duplicate = await _splitController.CreateSplit(0, 2, "Ogre");
        TestsHelper.SafetyChecks<ObjectResult>(duplicate);

        //Ensure error
        Assert.That((duplicate as ObjectResult)!.StatusCode!, Is.EqualTo(500));
        
        // ----- Different
        //Create run
        var different = await _splitController.CreateSplit(0, 1, "Ogre");
        TestsHelper.SafetyChecks(different);

        //Get Split from repo
        var run = await _splitRepository.GetSplit((int)(different as OkObjectResult)!.Value!);
        Assert.That(run, Is.Not.EqualTo(null));
    }

    //--------------- Rename Split  ---------------
    /// <summary>
    /// Renames the split "Genichiro" which has id 1, to "Start"
    /// This should succeed
    /// </summary>
    [Test]
    public async Task RenameSplit()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        Console.WriteLine(JsonSerializer.Serialize(await _accountRepository.GetAccount(0), options));
    }
}
using HitTrackerAPI.Controllers;
using HitTrackerAPI.Database;
using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.RunRepositories;
using HitTrackerAPI.Repositories.SplitRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerTest;

public class SplitControllerTests
{
    private AccountRepository _accountRepository = null!;
    private RunRepository _runRepository = null!;
    private SplitRepository _splitRepository = null!;

    private SplitController _splitController = null!;

    private readonly DbContextOptions<HitTrackerContext> _options =
        new DbContextOptionsBuilder<HitTrackerContext>().UseInMemoryDatabase("HitTracker").Options;

    private HitTrackerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        _context = new HitTrackerContext(_options);

        _accountRepository = new AccountRepository(_context);
        _runRepository = new RunRepository(_context);
        _splitRepository = new SplitRepository(_context);

        _splitController = new SplitController(_runRepository, _splitRepository);

        SeedDb.SeedingRun(_context);
    }

    [TearDown]
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
        var happy = await _splitController.CreateSplit(2, "Bull");
        TestsHelper.SafetyChecks(happy);

        //Check result of CreateSplit
        Assert.That((happy as OkObjectResult)!.Value, Is.EqualTo(4));
        Assert.That((await _splitRepository.GetSplit(4))?.ParentId, Is.EqualTo(2));

        // ----- Duplicate
        //Create "Bull" split in Sekiro
        var duplicate = await _splitController.CreateSplit(2, "Ogre");
        TestsHelper.SafetyChecks<ObjectResult>(duplicate);

        //Ensure error
        Assert.That((duplicate as ObjectResult)!.StatusCode!, Is.EqualTo(500));

        // ----- Different
        //Create run
        var different = await _splitController.CreateSplit(1, "Ogre");
        TestsHelper.SafetyChecks(different);

        //Get Split from repo
        var run = await _splitRepository.GetSplit((int)(different as OkObjectResult)!.Value!);
        Assert.That(run, Is.Not.EqualTo(null));
    }

    //--------------- Rename Split  ---------------
    /// <summary>
    /// Happy
    ///     Renames the split "Genichiro" which has id 1, to "Start"
    ///     This should succeed, since the name isn't taken
    /// Taken
    ///     Renames the split "Genichiro" which has id 1, to "Ogre"
    ///     This should fail, since the name is taken
    /// </summary>
    [Test]
    public async Task RenameSplit()
    {
        // ----- Happy
        //Rename
        var happy = await _splitController.RenameSplit(1, "Start");
        TestsHelper.SafetyChecks<OkResult>(happy);

        //Ensure
        Assert.That(
            (await _accountRepository.GetAccount(0))?.Runs?.Any(
                run => run.Splits!.Any(
                    split => split.Name == "Start")),
            Is.True);

        // ----- Taken
        //Rename
        var taken = await _splitController.RenameSplit(1, "Ogre");
        TestsHelper.SafetyChecks<ObjectResult>(taken);

        //Ensure
        Assert.That((taken as ObjectResult)!.StatusCode!, Is.EqualTo(500));
    }

    //--------------- Move Split  ---------------
    /// <summary>
    /// Moves Sekiro's "Genichiro" split to throughout the run a couple of times 
    /// Should clamp requested index within bounds of list, and move the surrounding splits
    /// </summary>
    [Test]
    public async Task MoveSplit()
    {
        //Move splitId 1 to the middle of the run
        var middle = await _splitController.MoveSplit(1, 1);
        TestsHelper.SafetyChecks<OkResult>(middle);
        TestsHelper.CheckSplitOrder((await _runRepository.GetRun(2))!, [2, 1, 3]);

        //Move splitId 1 to infinity
        var inf = await _splitController.MoveSplit(1, int.MaxValue);
        TestsHelper.SafetyChecks<OkResult>(inf);
        TestsHelper.CheckSplitOrder((await _runRepository.GetRun(2))!, [2, 3, 1]);

        //Move splitId 1 to negative infinity
        var negInf = await _splitController.MoveSplit(1, int.MinValue);
        TestsHelper.SafetyChecks<OkResult>(negInf);
        TestsHelper.CheckSplitOrder((await _runRepository.GetRun(2))!, [1, 2, 3]);
    }
    
    //--------------- Delete Split  ---------------
    /// <summary>
    /// Happy
    ///     Deletes the "Genichiro" split
    ///     This should succeed, since it still exists
    /// Gone
    ///     Tries to delete the "Genichiro" split again
    ///     This should fail, since it no longer exists
    /// </summary>
    [Test]
    public async Task DeleteSplit()
    {
        var delete = await _splitController.DeleteSplit(1);
        TestsHelper.SafetyChecks<OkResult>(delete);
        
        var gone = await _splitController.DeleteSplit(1);
        TestsHelper.SafetyChecks<NotFoundObjectResult>(gone);
    }
}
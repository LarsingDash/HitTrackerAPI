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
    /// Tries to create the split "Bull" on Sekiro on account 0
    /// The run doesn't have this split, so the created split should be id 4
    /// </summary>
    [Test]
    public async Task CreateSplit_Happy()
    {
        //Create "Bull" split in Sekiro
        var result = await _splitController.CreateSplit(0, 2, "Bull");
        
        //Safety Checks
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        
        //Check result of createSplit
        Assert.That((result as OkObjectResult)!.Value, Is.EqualTo(4));
    }
    
    /// <summary>
    /// Tries to create Ogre split on an account that already has it
    /// Should return with an error message indicating such
    /// </summary>
    [Test]
    public async Task CreateSplit_SameOnRun()
    {
        //Create "Bull" split in Sekiro
        var result = await _splitController.CreateSplit(0, 2, "Ogre");
        
        //Safety Checks
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<ObjectResult>());
        
        //Ensure error
        Assert.That((result as ObjectResult)!.StatusCode!, Is.EqualTo(500));
    }
    
    /// <summary>
    /// Tries to create an "Ogre" a run, while another run already has it 
    /// Should return still be allowed
    /// </summary>
    [Test]
    public async Task CreateSplit_SameDifferentAccount()
    {
        //Create run
        var result = await _splitController.CreateSplit(0, 1, "Ogre");

        //Safety Checks
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        //Get Split from repo
        var run = await _splitRepository.GetSplit((int)(result as OkObjectResult)!.Value!);
        Assert.That(run, Is.Not.EqualTo(null));
    }
}
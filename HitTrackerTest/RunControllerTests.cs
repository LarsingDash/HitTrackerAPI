using HitTrackerAPI.Controllers;
using HitTrackerAPI.Database;
using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.RunRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerTest;

public class RunControllerTests
{
    private AccountRepository _accountRepository = null!;
    private RunRepository _runRepository = null!;

    private RunController _runController = null!;

    private readonly DbContextOptions<HitTrackerContext> _options =
        new DbContextOptionsBuilder<HitTrackerContext>().UseInMemoryDatabase("HitTracker").Options;
    private HitTrackerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        _context = new HitTrackerContext(_options);

        _accountRepository = new AccountRepository(_context);
        _runRepository = new RunRepository(_context);

        _runController = new RunController(_accountRepository, _runRepository);

        SeedDb.SeedingRun(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    //--------------- Create Run  ---------------
    /// <summary>
    /// Happy
    ///     Tries to create the run "Elden Ring" on an account
    ///     The mock repository has the accounts 0 and 1, account 0 has the run "Dark Souls"
    /// Duplicate
    ///     Tries to create Dark Souls on an account that already has it
    ///     Should return with an error message indicating such
    /// Different
    ///     Tries to create Dark Souls on an account, while another already has it 
    ///     Should return still be allowed
    /// </summary>
    [Test]
    public async Task CreateRun()
    {
        // ----- Happy
        //Attempt to add the "Elden Ring", this should create the run with ID 1
        var happy = await _runController.CreateRun(0, "Elden Ring");
        TestsHelper.SafetyChecks(happy);

        //Assert its presence on GetRun
        var happyRun = await _runRepository.GetRun((int)(happy as OkObjectResult)!.Value!);
        Assert.That(happyRun, Is.Not.EqualTo(null));

        //Assert its presence in the account
        Assert.That(
            (await _accountRepository.GetAccount(0))?
            .Runs?.FirstOrDefault(
                r => r.Name == "Elden Ring")
            , Is.Not.EqualTo(null)
        );
        
        // ----- Duplicate
        //Create run
        var duplicate = await _runController.CreateRun(0, "Dark Souls");
        TestsHelper.SafetyChecks<ObjectResult>(duplicate);

        //Ensure error
        Assert.That((duplicate as ObjectResult)!.StatusCode!, Is.EqualTo(500));
        
        // ----- Different
        //Create run
        var different = await _runController.CreateRun(1, "Dark Souls");
        TestsHelper.SafetyChecks(different);

        //Get run form repo
        var differentRun = await _runRepository.GetRun((int)(different as OkObjectResult)!.Value!);
        Assert.That(differentRun, Is.Not.EqualTo(null));
    }
}
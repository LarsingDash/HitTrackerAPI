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

    private HitTrackerContext _context = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HitTrackerContext>().UseInMemoryDatabase("HitTracker").Options;
        _context = new HitTrackerContext(options);

        _accountRepository = new AccountRepository(_context);
        _runRepository = new RunRepository(_context);

        _runController = new RunController(_accountRepository, _runRepository);

        SeedDb.SeedingRun(_context);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    //--------------- Create Run  ---------------
    /// <summary>
    /// Tries to create the run "Elden Ring" on an account
    /// The mock repository has the accounts 0 and 1, account 0 has the run "Dark Souls"
    /// </summary>
    [Test]
    public async Task CreateRun_Happy()
    {
        //Attempt to add the "Elden Ring", this should create the run with ID 1
        var result = await _runController.CreateRun(0, "Elden Ring");

        //Assert success state
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        //Assert its presence on GetRun
        var run = await _runRepository.GetRun((int)(result as OkObjectResult)!.Value!);
        Assert.That(run, Is.Not.EqualTo(null));

        //Assert its presence in the account
        Assert.That(
            (await _accountRepository.GetAccount(0))?
            .Runs?.FirstOrDefault(
                r => r.Name == "Elden Ring")
            , Is.Not.EqualTo(null)
        );
    }

    /// <summary>
    /// Tries to create Dark Souls on an account that already has it
    /// Should return with an error message indicating such
    /// </summary>
    [Test]
    public async Task CreateRun_SameOnAccount()
    {
        //Create run
        var result = await _runController.CreateRun(0, "Dark Souls");

        //Safety Checks
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<ObjectResult>());

        //Ensure error
        Assert.That((result as ObjectResult)!.StatusCode!, Is.EqualTo(500));
    }

    /// <summary>
    /// Tries to create Dark Souls on an account, while another already has it 
    /// Should return still be allowed
    /// </summary>
    [Test]
    public async Task CreateRun_SameDifferentAccount()
    {
        //Create run
        var result = await _runController.CreateRun(1, "Dark Souls");

        //Safety Checks
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        //Get run form repo
        var run = await _runRepository.GetRun((int)(result as OkObjectResult)!.Value!);
        Assert.That(run, Is.Not.EqualTo(null));
    }
}
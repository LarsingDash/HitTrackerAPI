using HitTrackerAPI.Controllers;
using HitTrackerAPI.Database;
using HitTrackerAPI.Repositories.AccountRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HitTrackerTest;

public class AccountControllerTests
{
    private AccountRepository _repository = null!;
    private AccountController _controller = null!;

    private readonly DbContextOptions<HitTrackerContext> _options =
        new DbContextOptionsBuilder<HitTrackerContext>().UseInMemoryDatabase("HitTracker").Options;
    private HitTrackerContext _context = null!;
    
    [SetUp]
    public void Setup()
    {
        _context = new HitTrackerContext(_options);
        
        _repository = new AccountRepository(_context);
        _controller = new AccountController(_repository);
        
        SeedDb.SeedingRun(_context);
    }
    
    [TearDown]
    public void Teardown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
    //--------------- Get Account ---------------
    /// <summary>
    /// Tries to fetch an account with a correct and bad id
    /// Correct should return OkObjectResult, bad should return NotFoundObjectResult
    /// </summary>
    [Test]
    public async Task GetAccount()
    {
        //Correct
        var correct = await _controller.GetFullAccount(0);
        Assert.That(correct, Is.InstanceOf<OkObjectResult>());
        
        //Bad
        var bad = await _controller.GetFullAccount(10);
        Assert.That(bad, Is.InstanceOf<NotFoundObjectResult>());
    }

    //--------------- Create Account ---------------
    /// <summary>
    /// Tries to create an account
    /// Since the mock repository has the accounts 0 and 1, this should make an account with ID 2
    /// </summary>
    [Test]
    public async Task CreateAccount()
    {
        //Create the account
        var firstResult = await _controller.CreateAccount();

        //Assert that the result was Ok
        Assert.That(firstResult, Is.Not.EqualTo(null));
        Assert.That(firstResult, Is.InstanceOf<OkObjectResult>());

        //Assert that the account (with ID 2) was successfully created
        Assert.That((firstResult as OkObjectResult)!.Value, Is.EqualTo(2));
        
        //Try the same again to make sure (now ID 3 should be created)
        var secondResult = await _controller.CreateAccount();
        Assert.That(secondResult, Is.Not.EqualTo(null));
        Assert.That(secondResult, Is.InstanceOf<OkObjectResult>());
        Assert.That((secondResult as OkObjectResult)!.Value, Is.EqualTo(3));
    }
}
using HitTrackerAPI.Controllers;
using HitTrackerAPI.Database;
using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.HitRepositories;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerTest;

public class HitControllerTests
{
    private AccountRepository _accountRepository = null!;
    private HitRepository _hitRepository = null!;

    private HitController _hitController = null!;

    private readonly DbContextOptions<HitTrackerContext> _options =
        new DbContextOptionsBuilder<HitTrackerContext>().UseInMemoryDatabase("HitTracker").Options;
    private HitTrackerContext _context = null!;
    
    [SetUp]
    public void Setup()
    {
        _context = new HitTrackerContext(_options);

        _accountRepository = new AccountRepository(_context);
        _hitRepository = new HitRepository();

        _hitController = new HitController(_accountRepository, _hitRepository);

        SeedDb.SeedingRun(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
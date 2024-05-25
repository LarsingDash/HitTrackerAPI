using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.HitRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HitController(IAccountRepository accountRepo, IHitRepository hitRepo) : ControllerBase
{
    
}
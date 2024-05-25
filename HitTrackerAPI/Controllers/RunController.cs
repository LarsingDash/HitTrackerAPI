using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.RunRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Tags("2 - Runs")]
[Route("api/[controller]")]
public class RunController(IAccountRepository accountRepo, IRunRepository runRepo) : ControllerBase
{
    /// <summary>
    /// Requests creation of a run
    /// </summary>
    /// <param name="accountId">Id of the calling account</param>
    /// <param name="name">Name of the run that will be created</param>
    /// <response code="200">A run with <i><b>id</b></i> was successfully created</response>
    /// <response code="500">There was an error while creating the run</response>
    /// <response code="404">Could not find an account with the given <i><b>id</b></i></response>
    [HttpPost("CreateRun")]
    public async Task<IActionResult> CreateRun(int accountId, string name)
    {
        //Check account
        var account = await accountRepo.GetAccount(accountId);
        if (account == null) return NotFound("Could not find an account with the given id");

        //Create run and add to the account
        var result = await runRepo.CreateRun(account, name);
        return result != null ? Ok(result) : StatusCode(500, "Error creating run");
    }
}
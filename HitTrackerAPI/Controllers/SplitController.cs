using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.SplitRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SplitController(IAccountRepository accountRepo, ISplitRepository splitRepo) : ControllerBase
{
    /// <summary>
    /// Requests creation of a split
    /// </summary>
    /// <param name="accountId">Id of the calling account</param>
    /// <param name="runId">Id of the run which the split will belong to</param>
    /// <param name="name">Name of the split that will be created</param>
    /// <response code="200">A split with <i><b>id</b></i> was successfully created</response>
    /// <response code="500">There was an error while creating the split</response>
    /// <response code="404">Could not find an account with the given <i><b>id</b></i></response>
    [HttpPost("CreateSplit")]
    public async Task<IActionResult> CreateSplit(int accountId, int runId, string name)
    {
        //Check account
        var account = await accountRepo.GetAccount(accountId);
        if (account == null) return NotFound("Could not find an account with the given id");

        //Create run and add to the account
        var result = await splitRepo.CreateSplit(account, runId, name);
        return result != null ? Ok(result) : StatusCode(500, "Error creating run");
    }
}
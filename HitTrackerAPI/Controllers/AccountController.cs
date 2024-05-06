using HitTrackerAPI.Models;
using HitTrackerAPI.Repositories.AccountRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountRepository accountRepo) : ControllerBase
{
    /// <summary>
    /// Requests creation of an account
    /// </summary>
    /// <response code="200">An account with <i><b>"id"</b></i> was successfully created</response>
    /// <response code="400">There was an error while creating the account</response>
    [HttpPost("CreateAccount")]
    public async Task<IActionResult> CreateAccount()
    {
        //Find first available ID
        var id = 0;
        while (await accountRepo.GetAccount(id) != null) id++;

        //Create account with the ID
        if (await accountRepo.CreateAccount(new Account { AccountId = id }))
            return Ok(id);
        
        //Error
        return BadRequest("Error creating account");
    }
}
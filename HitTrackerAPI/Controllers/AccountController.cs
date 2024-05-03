using HitTrackerAPI.Models;
using HitTrackerAPI.Repositories.AccountRepositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        // if (await accountRepo.GetAccount(id) != null)
            // return BadRequest("Account with the given ID already exists");

        // if (await accountRepo.CreateAccount(new Account { AccountId = id }))
            // return Ok();
        // return BadRequest("Error creating account");

        return BadRequest("Not implemented");
    }
}
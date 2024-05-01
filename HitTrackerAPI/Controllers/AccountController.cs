using HitTrackerAPI.Models;
using HitTrackerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountRepository accountRepo) : ControllerBase
{
    /// <summary>
    /// Requests creation of an account with the given ID
    /// </summary>
    /// <param name="id">The Discord ID of the account that will be created</param>
    /// <response code="200">The account was successfully created</response>
    /// <response code="400">An account with the given ID already exists</response>
    [HttpPost("CreateAccount/{id:int}")]
    public async Task<IActionResult> CreateAccount(
        [SwaggerParameter(Description = "The Discord ID of the account that will be created")] int id)
    {
        if (await accountRepo.GetAccount(id) != null)
            return BadRequest("Account with the given ID already exists");

        await accountRepo.CreateAccount(new Account { AccountId = id });
        return Ok();
    }
}
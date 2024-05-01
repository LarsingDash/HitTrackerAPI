using HitTrackerAPI.Models;
using HitTrackerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountRepository accountRepo) : ControllerBase
{
    [HttpPost("CreateAccount")]
    public async Task<IActionResult> CreateAccount(int id)
    {
        if (await accountRepo.GetAccount(id) != null)
            return BadRequest("Account with the given ID already exists");

        await accountRepo.CreateAccount(new Account { AccountId = id });
        return Ok();
    }
}
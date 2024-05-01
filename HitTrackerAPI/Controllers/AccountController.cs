using HitTrackerAPI.Models;
using HitTrackerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountRepository accountRepository) : ControllerBase
{
    [HttpPost("CreateAccount")]
    public async Task<IActionResult> CreateAccount(int id)
    {
        //Todo check if already exists
        await accountRepository.CreateAccount(new Account { AccountId = id });
        return Ok();
    }
}
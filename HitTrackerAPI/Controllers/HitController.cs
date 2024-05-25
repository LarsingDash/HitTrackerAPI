using HitTrackerAPI.Repositories.AccountRepositories;
using HitTrackerAPI.Repositories.HitRepositories;
using HitTrackerAPI.Repositories.SplitRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Tags("4 - Hits")]
[Route("api/[controller]")]
public class HitController(IHitRepository hitRepo, ISplitRepository splitRepo) : ControllerBase
{
    /// <summary>
    /// Requests creation of a hit
    /// </summary>
    /// <param name="splitId">Id of the parenting split</param>
    /// <param name="message">Message that will be added to the created hit</param>
    /// <response code="200">The new amount of hits on the split</response>
    /// <response code="500">There was an error while creating the hit</response>
    /// <response code="404">Could not find a split with the given <i><b>id</b></i></response>
    [HttpPost("CreateHit")]
    public async Task<IActionResult> CreateHit(int splitId, string message)
    {
        var split = await splitRepo.GetSplit(splitId);
        if (split == null) return NotFound("Could not find a split with the given id");

        var result = await hitRepo.CreateHit(split, message);
        return result != null ? Ok(result) : StatusCode(500, "There was an error while creating the hit");
    }
}
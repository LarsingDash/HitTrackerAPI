using HitTrackerAPI.Repositories.RunRepositories;
using HitTrackerAPI.Repositories.SplitRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerAPI.Controllers;

[ApiController]
[Tags("3 - Splits")]
[Route("api/[controller]")]
public class SplitController(IRunRepository runRepo, ISplitRepository splitRepo) : ControllerBase
{
    /// <summary>
    /// Requests creation of a split
    /// </summary>
    /// <param name="runId">Id of the run which the split will belong to</param>
    /// <param name="name">Name of the split that will be created</param>
    /// <response code="200">A split with <i><b>id</b></i> was successfully created</response>
    /// <response code="500">There was an error while creating the split</response>
    /// <response code="404">Could not find a run with the given <i><b>id</b></i></response>
    [HttpPost("CreateSplit")]
    public async Task<IActionResult> CreateSplit(int runId, string name)
    {
        //Check account
        var run = await runRepo.GetRun(runId);
        if (run == null) return NotFound("Could not find a run with the given id");

        //Create run and add to the account
        var result = await splitRepo.CreateSplit(run, name);
        return result != null ? Ok(result) : StatusCode(500, "Error creating run");
    }

    /// <summary>
    /// Renames the split with the given id to the given name
    /// </summary>
    /// <param name="splitId">Id of the split to rename</param>
    /// <param name="name">New name for the split, must not be already taken</param>
    /// <response code="200">The split was successfully renamed</response>
    /// <response code="404">Could not find a split or parenting run with the given <i><b>id</b></i></response>
    /// <response code="500">The requested name was already taken</response>
    [HttpPatch("RenameSplit")]
    public async Task<IActionResult> RenameSplit(int splitId, string name)
    {
        //GetSplit
        var split = await splitRepo.GetSplit(splitId);
        if (split == null) return NotFound("Could not find a split with the given id");

        //GetRun
        var run = await runRepo.GetRun(split.ParentId);
        if (run == null) return NotFound("Could not find parenting run with the given id");
        
        //RenameSplit
        var result = await splitRepo.RenameSplit(split, run, name);
        return result ? Ok() : StatusCode(500, "Name was already taken");
    }

    /// <summary>
    /// Moves the split with the given id to the given position
    /// </summary>
    /// <param name="splitId">Id of the split to move</param>
    /// <param name="runPosition">New position for the split, indices start from 0</param>
    /// <response code="200">The split was successfully moved</response>
    /// <response code="404">Could not find a split or parenting run with the given <i><b>id</b></i></response>
    [HttpPatch("MoveSplit")]
    public async Task<IActionResult> MoveSplit(int splitId, int runPosition)
    {
        //GetSplit
        var split = await splitRepo.GetSplit(splitId);
        if (split == null) return NotFound("Could not find a split with the given id");
        
        //GetRun
        var run = await runRepo.GetRun(split.ParentId);
        if (run == null) return NotFound("Could not find parenting run with the given id");
        
        //MoveSplit
        var result = await splitRepo.MoveSplit(split, run, runPosition);
        return result ? Ok() : StatusCode(500, "An error occurred while moving the split");
    }

    /// <summary>
    /// Deletes the split with the given id
    /// </summary>
    /// <param name="splitId">Id of the split to delete</param>
    /// <response code="200">The split was successfully deleted</response>
    /// <response code="404">Could not find a split or parenting run with the given <i><b>id</b></i></response>
    /// <response code="500">An error occured while deleting the split</response>
    [HttpDelete("DeleteSplit")]
    public async Task<IActionResult> DeleteSplit(int splitId)
    {
        //GetSplit
        var split = await splitRepo.GetSplit(splitId);
        if (split == null) return NotFound("Could not find a split with the given id");
        
        //GetRun
        var run = await runRepo.GetRun(split.ParentId);
        if (run == null) return NotFound("Could not find parenting run with the given id");
        
        var result = await splitRepo.DeleteSplit(split, run);
        return result ? Ok() : StatusCode(500, "An error occured while deleting the split");
    }
}
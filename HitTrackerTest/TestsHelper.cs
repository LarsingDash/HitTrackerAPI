using HitTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerTest;

public static class TestsHelper
{
    public static void SafetyChecks(IActionResult result)
    {
        SafetyChecks<OkObjectResult>(result);
    }
    
    public static void SafetyChecks<T>(IActionResult result)
    {
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<T>(), () => (result as ObjectResult)?.Value?.ToString());
    }

    public static void CheckSplitOrder(Run run, List<int> expected)
    {
        List<Split> actualSplits = [];
        actualSplits.AddRange(run.Splits!);
        var actualOrder = actualSplits.OrderBy(split => split.Order).Select(split => split.SplitId).ToList();
        
        Assert.That(actualOrder, Is.EqualTo(expected));
    }
}
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
        Assert.That(result, Is.InstanceOf<T>());
    }
}
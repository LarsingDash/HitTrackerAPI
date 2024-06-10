using System.Text.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace HitTrackerAPI.Models;

public class RunDto
{
    public string Name { get; set; } = null!;
    public int? HitCount { get; set; }

    public static RunDto FromRun(Run run)
    {
        int? amount = 0;

        if (run.Splits == null) amount = null;
        else
            foreach (var split in run.Splits)
            {
                if (split.Hits == null) amount = null;
                else amount += split.Hits.Count;
            }

        return new RunDto { Name = run.Name, HitCount = amount };
    }

    public override string ToString() => JsonSerializer.Serialize(this, Program.Options);
}

public class SplitDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<HitDto>? Hits { get; set; }

    public static SplitDto FromSplit(Split split) => new SplitDto
    {
        Id = split.SplitId,
        Name = split.Name,
        Hits = split.Hits?.Select(HitDto.FromHit).ToList()
    };


    public override string ToString() => JsonSerializer.Serialize(this, Program.Options);
}

public class HitDto
{
    public DateTime TimeStamp { get; set; }
    public string Message { get; set; } = null!;

    public static HitDto FromHit(Hit hit) => new HitDto
    {
        TimeStamp = hit.Timestamp,
        Message = hit.Message
    };
    
    public override string ToString() => JsonSerializer.Serialize(this, Program.Options);
}
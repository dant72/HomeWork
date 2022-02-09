using HttpModels;

namespace HttpModels;

public class MyTime : IMyTime
{
    public DateTime LocalTime => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone);

    public TimeZoneInfo TimeZone { get; set; } = TimeZoneInfo.Utc;
}
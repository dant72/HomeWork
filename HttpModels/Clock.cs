using HttpModels;

namespace HttpModels;

public class Clock : IClock
{
    public DateTime LocalTimeNow => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone);

    public DateTime UtcNow => DateTime.UtcNow;

    public TimeZoneInfo TimeZone { get; set; } = TimeZoneInfo.Utc;
    
    public DateTime DiscountDate { get; set; } = DateTime.Now + TimeSpan.FromMinutes(5);
}
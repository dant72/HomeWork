namespace HttpModels;

public interface IClock
{
    public DateTime LocalTimeNow { get; }

    public TimeZoneInfo TimeZone { get; set; }

    public DateTime UtcNow { get; }

    public DateTime DiscountDate { get; set; }
}
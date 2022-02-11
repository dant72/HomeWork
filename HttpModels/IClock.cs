namespace HttpModels;

public interface IClock
{
    public DateTime LocalTime { get; }

    public TimeZoneInfo TimeZone { get; set; }
}
namespace HttpModels;

public interface IMyTime
{
    public DateTime LocalTime { get; }

    public TimeZoneInfo TimeZone { get; set; }
}
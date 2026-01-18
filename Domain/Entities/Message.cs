using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Message
{
    public int Id { get; set; }
    [JsonPropertyName("_id")]
    public string DeviceName { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string SessionName { get; set; } = string.Empty;
    [JsonPropertyName("startTime")]
    public DateTimeOffset StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public DateTimeOffset EndTime { get; set; }
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
}       
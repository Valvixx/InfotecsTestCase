using System.Text.Json.Serialization;

namespace Application.DTO.Message;

public class MessageGet
{
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
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Message
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("startTime")]
    public DateTimeOffset  StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public DateTimeOffset EndTime { get; set; }
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
}
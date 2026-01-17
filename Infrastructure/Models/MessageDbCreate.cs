using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public record class MessageDbCreate
{
    public string DeviceName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string Version { get; set; } = string.Empty;
}
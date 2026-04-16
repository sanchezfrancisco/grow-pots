using SmartPot.Api.Models;

namespace SmartPot.Api.DTOs;

/// <summary>
/// Data Transfer Object for the POST /api/smartpot/data endpoint.
/// Mirrors PlantData but without server-managed fields (Id, CreatedAt).
/// </summary>
public record PlantDataRequest(
    string DeviceId,
    float SoilMoisture,
    float Temperature,
    float AirHumidity,
    float LightLevel,
    DateTime? RecordedAt
)
{
    public PlantData ToModel() => new()
    {
        DeviceId     = DeviceId,
        SoilMoisture = SoilMoisture,
        Temperature  = Temperature,
        AirHumidity  = AirHumidity,
        LightLevel   = LightLevel,
        RecordedAt   = RecordedAt ?? DateTime.UtcNow,
        CreatedAt    = DateTime.UtcNow
    };
}

namespace SmartPot.Api.Models;

/// <summary>
/// Represents a sensor data reading sent by an ESP32 device.
/// </summary>
public class PlantData
{
    public int Id { get; set; }

    /// <summary>Unique identifier of the ESP32 device / pot.</summary>
    public string DeviceId { get; set; } = string.Empty;

    /// <summary>Soil moisture percentage (0–100).</summary>
    public float SoilMoisture { get; set; }

    /// <summary>Ambient temperature in °C.</summary>
    public float Temperature { get; set; }

    /// <summary>Ambient humidity percentage (0–100).</summary>
    public float AirHumidity { get; set; }

    /// <summary>Light level in lux.</summary>
    public float LightLevel { get; set; }

    /// <summary>UTC timestamp when the reading was taken on the device.</summary>
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    /// <summary>UTC timestamp when the record was inserted in the database.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

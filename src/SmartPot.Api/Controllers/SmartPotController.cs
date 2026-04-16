using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPot.Api.Data;
using SmartPot.Api.DTOs;
using SmartPot.Api.Models;


namespace SmartPot.Api.Controllers;

[ApiController]
[Route("api/smartpot")]
[Produces("application/json")]
public class SmartPotController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ILogger<SmartPotController> _logger;

    public SmartPotController(AppDbContext db, ILogger<SmartPotController> logger)
    {
        _db     = db;
        _logger = logger;
    }

    /// <summary>
    /// Receives sensor data from an ESP32 device and stores it in the database.
    /// </summary>
    /// <param name="request">Sensor reading payload from the ESP32.</param>
    /// <returns>The stored reading with its assigned Id.</returns>
    /// <response code="201">Record created successfully.</response>
    /// <response code="400">Invalid payload.</response>
    [HttpPost("data")]
    [ProducesResponseType(typeof(PlantData), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostData([FromBody] PlantDataRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.DeviceId))
            return BadRequest(new { error = "DeviceId is required." });

        var reading = request.ToModel();

        _db.PlantReadings.Add(reading);
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "📡 New reading from device {DeviceId} — Moisture: {Moisture}%, Temp: {Temp}°C",
            reading.DeviceId, reading.SoilMoisture, reading.Temperature);

        return CreatedAtAction(nameof(GetLatest), new { deviceId = reading.DeviceId }, reading);
    }

    /// <summary>
    /// Returns the latest reading for a given device.
    /// </summary>
    /// <param name="deviceId">The ESP32 device identifier.</param>
    [HttpGet("data/{deviceId}/latest")]
    [ProducesResponseType(typeof(PlantData), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLatest(string deviceId)
    {
        var latest = await _db.PlantReadings
            .Where(p => p.DeviceId == deviceId)
            .OrderByDescending(p => p.RecordedAt)
            .FirstOrDefaultAsync();

        return latest is null
            ? NotFound(new { error = $"No readings found for device '{deviceId}'." })
            : Ok(latest);
    }
}

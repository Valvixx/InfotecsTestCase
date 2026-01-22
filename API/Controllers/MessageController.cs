using Application.DTO.Message;
using Application.Services.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/message")]
public class MessageController(IMessageService messageService, ILogger<MessageController> logger) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] MessageCreate messageCreate)
    {
        try
        {
            if (messageCreate == null)
            {
                return BadRequest("Request body cannot be empty.");
            }

            if (string.IsNullOrEmpty(messageCreate.DeviceName))
            {
                return BadRequest("DeviceName is required.");
            }

            if (string.IsNullOrEmpty(messageCreate.SessionName))
            {
                return BadRequest("SessionName is required.");
            }

            if (messageCreate.StartTime > messageCreate.EndTime)
            {
                return BadRequest("StartTime cannot be later than EndTime.");
            }

            await messageService.CreateAsync(messageCreate);
            return Ok();
        }
        catch (ValidationException ex)
        {
            logger.LogWarning("Validation error in CreateAsync: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning("Not found error in CreateAsync: {Message}", ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning("Unauthorized access in CreateAsync: {Message}", ex.Message);
            return StatusCode(403, new { error = "Access denied." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error in CreateAsync");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpGet("all_devices")]
    public async Task<IActionResult> GetAllDevicesAsync()
    {
        try
        {
            DevicesListGet devices = await messageService.GetAllDevicesAsync();
            return Ok(devices);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning("Validation error in GetAllDevicesAsync: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning("Not found error in GetAllDevicesAsync: {Message}", ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning("Unauthorized access in GetAllDevicesAsync: {Message}", ex.Message);
            return StatusCode(403, new { error = "Access denied." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error in GetAllDevicesAsync");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpGet("messages_by_device")]
    public async Task<IActionResult> GetAllMessagesByDeviceNameAsync(string deviceName)
    {
        try
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                return BadRequest("DeviceName parameter is required.");
            }

            List<MessageGet> messages = await messageService.GetAllMessagesByDeviceNameAsync(deviceName);
            return Ok(messages);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning("Validation error in GetAllMessagesByDeviceNameAsync: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning("Not found error in GetAllMessagesByDeviceNameAsync: {Message}", ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning("Unauthorized access in GetAllMessagesByDeviceNameAsync: {Message}", ex.Message);
            return StatusCode(403, new { error = "Access denied." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error in GetAllMessagesByDeviceNameAsync");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}   
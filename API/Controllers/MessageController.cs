using Application.DTO.Message;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/message")]
public class MessageController (IMessageService messageService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] MessageCreate messageCreate)
    {
        await messageService.CreateAsync(messageCreate);
        return Ok();
    }

    [HttpGet("all_devices")]
    public async Task<IActionResult> GetAllDevicesAsync()
    {
        DevicesListGet devices = await messageService.GetAllDevicesAsync();
        return Ok(devices);
    }

    [HttpGet("messages_by_device")]
    public async Task<IActionResult> GetAllMessagesByDeviceNameAsync(string deviceName)
    {
        List<MessageGet> messages = await messageService.GetAllMessagesByDeviceNameAsync(deviceName);
        return Ok(messages);
    }
}   
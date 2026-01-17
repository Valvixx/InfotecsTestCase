using Application.DTO.Message;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController (IMessageService messageService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] MessageCreate messageCreate)
    {
        await messageService.CreateAsync(messageCreate);
        return Ok();
    }
}
using Application.DTO.Message;
using Domain.Entities;
using Infrastructure.Models;

namespace Application.Services.Interfaces;

public interface IMessageService
{
    Task CreateAsync(MessageCreate data);
    Task<DevicesListGet> GetAllDevicesAsync();
    Task<List<MessageGet>> GetAllMessagesByDeviceNameAsync(string deviceName);
}
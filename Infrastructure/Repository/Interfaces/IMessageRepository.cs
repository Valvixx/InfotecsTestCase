using Domain.Entities;
using Infrastructure.Models;

namespace Infrastructure.Repository.Interfaces;

public interface IMessageRepository
{
    public Task<List<DeviceDbGet>> GetAllDevicesAsync();
    public Task<List<Message>> GetAllMessagesByDeviceNameAsync(string deviceName);
    public Task CreateMessageAsync(MessageDbCreate data);
    public Task UpdateMessageAsync(Message message);
    public Task DeleteMessageAsync(Message message);
}
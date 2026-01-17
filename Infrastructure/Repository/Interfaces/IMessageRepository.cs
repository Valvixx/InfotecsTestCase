using Domain.Entities;
using Infrastructure.Models;

namespace Infrastructure.Repository.Interfaces;

public interface IMessageRepository
{
    public Task<List<string>> GetAllDevicesAsync();
    public Task<Message> GetAllMessagesByDeviceIdAsync(int deviceId);
    public Task CreateMessageAsync(MessageDbCreate data);
    public Task UpdateMessageAsync(Message message);
    public Task DeleteMessageAsync(Message message);
}
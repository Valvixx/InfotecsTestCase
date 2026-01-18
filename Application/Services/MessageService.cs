using Application.DTO.Message;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Models;
using Infrastructure.Repository.Interfaces;

namespace Application.Services;

public class MessageService(IMessageRepository messageRepository):IMessageService
{
    public async Task CreateAsync(MessageCreate data)
    {
        await messageRepository.CreateMessageAsync(new MessageDbCreate
        {
            DeviceName = data.DeviceName,
            SessionName = data.SessionName,
            StartTime = data.StartTime,
            EndTime = data.EndTime,
            Version = data.Version
        });
    }

    public async Task<List<DeviceGet>> GetAllDevicesAsync()
    {
        List<DeviceDbGet> devices = await messageRepository.GetAllDevicesAsync();

        return devices
            .Select(d => new DeviceGet
            {
                DeviceName = d.DeviceName
            })
            .ToList();
    }


    public async Task<List<MessageGet>> GetAllMessagesByDeviceNameAsync(string deviceName)
    {
        List<Message> messages = await messageRepository.GetAllMessagesByDeviceNameAsync(deviceName);
        return messages
            .Select(m => new MessageGet
            {
                DeviceName = m.DeviceName,
                SessionName = m.SessionName,
                StartTime = m.StartTime,
                EndTime = m.EndTime,
                Version = m.Version
                
            })
            .ToList();
    }
}
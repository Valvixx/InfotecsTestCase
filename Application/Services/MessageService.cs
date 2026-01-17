using Application.DTO.Message;
using Application.Services.Interfaces;
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
            Name = data.Name,
            StartTime = data.StartTime,
            EndTime = data.EndTime,
            Version = data.Version
        });
    }
}
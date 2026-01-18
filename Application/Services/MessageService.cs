using Application.DTO.Message;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Models;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class MessageService(
    IMessageRepository messageRepository, 
    ILogger<MessageService> logger) : IMessageService
{
    public async Task CreateAsync(MessageCreate data)
    {
        logger.LogInformation("Создание сообщения для устройства {DeviceName}", data.DeviceName);
        
        try
        {
            await messageRepository.CreateMessageAsync(new MessageDbCreate
            {
                DeviceName = data.DeviceName,
                SessionName = data.SessionName,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                Version = data.Version
            });
            
            logger.LogInformation("Сообщение успешно создано для {DeviceName}", data.DeviceName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка создания сообщения для {DeviceName}", data.DeviceName);
            throw;
        }
    }

    public async Task<DevicesListGet> GetAllDevicesAsync()
    {
        logger.LogInformation("Запрос списка всех устройств");
        
        try
        {
            List<DeviceDbGet> devices = await messageRepository.GetAllDevicesAsync();
        
            var result = new DevicesListGet
            {
                Devices = devices.Select(d => d.DeviceName).Distinct().ToList()
            };
            
            logger.LogInformation("Найдено {DeviceCount} уникальных устройств", result.Devices.Count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения списка устройств");
            throw;
        }
    }

    public async Task<List<MessageGet>> GetAllMessagesByDeviceNameAsync(string deviceName)
    {
        logger.LogInformation("Запрос сообщений для устройства {DeviceName}", deviceName);
        
        try
        {
            List<Message> messages = await messageRepository.GetAllMessagesByDeviceNameAsync(deviceName);
            
            var result = messages
                .Select(m => new MessageGet
                {
                    DeviceName = m.DeviceName,
                    SessionName = m.SessionName,
                    StartTime = m.StartTime,
                    EndTime = m.EndTime,
                    Version = m.Version
                })
                .ToList();
                
            logger.LogInformation("Найдено {MessageCount} сообщений для {DeviceName}", 
                result.Count, deviceName);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения сообщений для {DeviceName}", deviceName);
            throw;
        }
    }
}

using Application.DTO.Message;
using Application.Services.Interfaces;
using Domain;
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
        logger.LogInformation("Creating message for device {DeviceName}", data.DeviceName);

        try
        {
            if (string.IsNullOrWhiteSpace(data.DeviceName))
            {
                throw new ValidationException("Device name cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(data.SessionName))
            {
                throw new ValidationException("Session name cannot be null or empty.");
            }

            if (data.StartTime > data.EndTime)
            {
                throw new ValidationException("Start time cannot be later than end time.");
            }

            await messageRepository.CreateMessageAsync(new MessageDbCreate
            {
                DeviceName = data.DeviceName,
                SessionName = data.SessionName,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                Version = data.Version
            });

            logger.LogInformation("Message successfully created for {DeviceName}", data.DeviceName);
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating message for {DeviceName}", data.DeviceName);
            throw;
        }
    }

    public async Task<DevicesListGet> GetAllDevicesAsync()
    {
        logger.LogInformation("Requesting list of all devices");

        try
        {
            List<DeviceDbGet> devices = await messageRepository.GetAllDevicesAsync();

            var result = new DevicesListGet
            {
                Devices = devices.Select(d => d.DeviceName).Distinct().ToList()
            };

            logger.LogInformation("Found {DeviceCount} unique devices", result.Devices.Count);
            return result;
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting list of devices");
            throw;
        }
    }

    public async Task<List<MessageGet>> GetAllMessagesByDeviceNameAsync(string deviceName)
    {
        logger.LogInformation("Requesting messages for device {DeviceName}", deviceName);

        try
        {
            if (string.IsNullOrWhiteSpace(deviceName))
            {
                throw new ValidationException("Device name cannot be null or empty.");
            }

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

            logger.LogInformation("Found {MessageCount} messages for {DeviceName}",
                result.Count, deviceName);

            return result;
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting messages for {DeviceName}", deviceName);
            throw;
        }
    }
}
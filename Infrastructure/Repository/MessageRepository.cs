using System.Text.Json;
using Domain.Entities;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Scripts.Message;

namespace Infrastructure.Repository;

public class MessageRepository(IDapperContext dapperContext): IMessageRepository
{
    public async Task<List<DeviceDbGet>> GetAllDevicesAsync()
    {
        var query = new QueryObject(PostgresMessage.GetAllDevices, new {});
        return await dapperContext.ListOrEmpty<DeviceDbGet>(query) ?? new List<DeviceDbGet>();
    }

    public async Task<List<Message>> GetAllMessagesByDeviceNameAsync(string deviceName)
    {
        var query = new QueryObject(PostgresMessage.GetAllMessagesByDeviceName, new { DeviceName = deviceName });
        return await dapperContext.ListOrEmpty<Message>(query) ?? new List<Message>();
    }

    public async Task CreateMessageAsync(MessageDbCreate data)
    {
        var query = new QueryObject(PostgresMessage.Insert, data);
        await dapperContext.Command<Message>(query);
    }
}
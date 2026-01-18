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
        var query = new QueryObject("SELECT device_name AS \"DeviceName\" FROM devices", new {});
        return await dapperContext.ListOrEmpty<DeviceDbGet>(query) ?? new List<DeviceDbGet>();
    }

    public async Task<List<Message>> GetAllMessagesByDeviceNameAsync(string deviceName)
    {
        var sql = "SELECT\n" +
                  "    d.device_name AS \"DeviceName\",\n" +
                  "    s.session_name AS \"SessionName\",\n" +
                  "    s.start_time AS \"StartTime\",\n" +
                  "    s.end_time AS \"EndTime\",\n" +
                  "    s.version AS \"Version\"\n" +
                  "FROM sessions s\nJOIN devices d ON s.device_id=d.id\n" +
                  "WHERE d.device_name = @DeviceName;\n";
        var query = new QueryObject(sql, new { DeviceName = deviceName });
        return await dapperContext.ListOrEmpty<Message>(query) ?? new List<Message>();
    }

    public async Task CreateMessageAsync(MessageDbCreate data)
    {
        var query = new QueryObject(PostgresMessage.Insert, data);
        await dapperContext.Command<Message>(query);
    }

    public Task UpdateMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }
}
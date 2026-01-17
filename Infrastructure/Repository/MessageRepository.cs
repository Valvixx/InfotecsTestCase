using Domain.Entities;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Scripts.Message;

namespace Infrastructure.Repository;

public class MessageRepository(IDapperContext dapperContext): IMessageRepository
{
    public Task<List<string>> GetAllDevicesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Message> GetAllMessagesByDeviceIdAsync(int deviceId)
    {
        throw new NotImplementedException();
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
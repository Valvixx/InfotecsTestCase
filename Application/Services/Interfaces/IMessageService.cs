using Application.DTO.Message;

namespace Application.Services.Interfaces;

public interface IMessageService
{
    Task CreateAsync(MessageCreate data);
}
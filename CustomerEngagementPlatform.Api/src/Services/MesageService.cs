using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerEngagementPlatform.Api.src.Models;
using CustomerEngagementPlatform.Api.src.Repositories;
using CustomerEngagementPlatform.Api.src.Services;

namespace CustomerEngagementPlatform.Api.src.Services
{
    public class MessageService(IMessageRepository messageRepository) : IMessageService
    {
        private readonly IMessageRepository _messageRepository = messageRepository;

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _messageRepository.GetAllMessagesAsync();
        }

        public async Task<Message?> GetMessageByIdAsync(int messageId)
        {
            return await _messageRepository.GetMessageByIdAsync(messageId);
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messageRepository.AddMessageAsync(message);
        }

        public async Task UpdateMessageAsync(Message message)
        {
            await _messageRepository.UpdateMessageAsync(message);
        }

        public async Task DeleteMessageAsync(int messageId)
        {
            await _messageRepository.DeleteMessageAsync(messageId);
        }
    }
}


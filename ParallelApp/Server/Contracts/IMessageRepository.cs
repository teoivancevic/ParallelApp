using ParallelApp.Shared.Dto;
using ParallelApp.Shared.Models;

namespace ParallelApp.Server.Contracts
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<Message>> GetUserFeed(int user_id);
        public Task CreateMessage(MessageForCreationDto message);
        public Task AddMessageTags(List<int> tagIds);
    }
}

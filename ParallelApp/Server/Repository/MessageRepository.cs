using Dapper;
using ParallelApp.Server.Contracts;
using ParallelApp.Server.Models;
using ParallelApp.Shared.Models;
using ParallelApp.Shared.Dto;
using System.Data;

namespace ParallelApp.Server.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DapperContext _context;
        public MessageRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetUserFeed(int user_id)
        {
            var query1 = "SELECT Messages.Id, Messages.Subject, Messages.Content, Messages.SenderUserId, Messages.SchoolId, Messages.Likes, Messages.Created FROM UserTags " +
                         "JOIN Tags " +
                         "    ON Tags.Id = UserTags.TagId " +
                         "JOIN MessageTags " +
                         "    ON MessageTags.TagId = Tags.Id " +
                         "JOIN Messages " +
                         "    ON Messages.Id = MessageTags.MessageId " +
                         "WHERE UserTags.UserId = @user_id " +
                         "GROUP BY Messages.Id " +
                         "ORDER BY Messages.Id DESC ";

            

            using (var connection = _context.CreateConnection())
            {
                var messages = await connection.QueryAsync<Message>(query1, new {user_id});
                //return messages.ToList();
                List<Message> messagesWithTags = new List<Message>();
                foreach(var message in messages.ToList())
                {
                    var query2 = "SELECT Tags.Id, Tags.Name, Tags.SchoolId, Tags.Type, Tags.Created, Tags.Color FROM Tags " +
                                 "JOIN MessageTags " +
                                 "    ON MessageTags.TagId = Tags.Id " +
                                 "JOIN Messages " +
                                 "    ON Messages.Id = MessageTags.MessageId " +
                                 "WHERE Messages.Id = @MessageId ";
                    
                    var parameters = new DynamicParameters();
                    parameters.Add("MessageId", message.Id, DbType.Int32);

                    var tags = await connection.QueryAsync<Tag>(query2, parameters);
                    message.Tags = tags.ToList();
                    messagesWithTags.Add(message);
                }

                return messagesWithTags;
            }
        }

        public async Task CreateMessage(MessageForCreationDto message)
        {
            var query = "INSERT INTO Messages (Subject, Content, SchoolId, SenderUserId, Likes) VALUES (@Subject, @Content, @SchoolId, @SenderUserId, @Likes)";

            var parameters = new DynamicParameters();
            parameters.Add("Subject", message.Subject, DbType.String);
            parameters.Add("Content", message.Content, DbType.String);
            parameters.Add("SchoolId", message.SchoolId, DbType.Int32);
            parameters.Add("SenderUserId", message.SenderUserId, DbType.Int32);
            parameters.Add("Likes", message.Likes, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task AddMessageTags(List<int> tagIds)
        {
            using (var connection = _context.CreateConnection())
            {
                var query1 = "SELECT Id FROM Messages ORDER BY Id DESC LIMIT 1";
                int message_id = await connection.QuerySingleOrDefaultAsync<int>(query1);
                foreach (int tagId in tagIds)
                {
                    string query2 = "INSERT INTO MessageTags (MessageId, TagId) VALUES (@MessageId, @TagId)";
                    
                    var parameters = new DynamicParameters();
                    parameters.Add("MessageId", message_id, DbType.Int32);
                    parameters.Add("TagId", tagId, DbType.Int32);
                    
                    connection.ExecuteAsync(query2, parameters);
                }
            }
        }

    }
}

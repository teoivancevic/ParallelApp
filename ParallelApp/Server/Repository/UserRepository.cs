using Dapper;
using ParallelApp.Server.Contracts;

using ParallelApp.Server.Models;
using ParallelApp.Shared;
using ParallelApp.Shared.Models;
//using ParallelApp.Shared.Dto;
using System.Data;

namespace ParallelApp.Server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> GetUserId(string auth0Id)
        {
            var query = "SELECT Id FROM Users WHERE HrEduUserPersistentID = @auth0Id";
            using (var connection = _context.CreateConnection())
            {
                var user_id = await connection.QuerySingleOrDefaultAsync<int>(query, new { auth0Id });
                return user_id;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = "SELECT * FROM Users";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<IEnumerable<User>> GetUsersWithTags(int school_id)
        {
            //var query = "SELECT * FROM Users WHERE SchoolId = @school_id";
            var query0 = "SELECT Users.Id, Users.FirstName, Tags.Name FROM Users " +
                        "LEFT JOIN UserTags " +
                        "   ON Users.Id = UserTags.UserId " +
                        "LEFT JOIN Tags " +
                        "   ON Tags.Id = UserTags.TagId " +
                        "WHERE Users.SchoolId = @school_id " +
                        "GROUP BY Users.Id";

            var query1 = "SELECT * FROM Users WHERE SchoolId = @school_id";
            var query2 = "SELECT Tags.Id, Tags.Name, Tags.SchoolId, Tags.Type, Tags.Created, Tags.Color FROM Tags " +
                         "JOIN UserTags " + 
                         "  ON UserTags.TagId = Tags.Id " +
                         "WHERE UserTags.UserId = @UserId ";

            var query3 = "SELECT Roles.Id, Roles.Name, Roles.Created FROM Roles " +
                         "JOIN UserRoles " +
                         "  ON Roles.Id = UserRoles.RoleId " +
                         "WHERE UserRoles.UserId = @UserId";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query1, new { school_id });
                List<User> detailedUsers = new List<User>();
                foreach(var user in users)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", user.Id, DbType.Int32);
                    user.Tags = (await connection.QueryAsync<Tag>(query2, parameters)).ToList();

                    user.Role = await connection.QuerySingleOrDefaultAsync<Role>(query3, parameters);

                    detailedUsers.Add(user);
                }
                return detailedUsers;
            }
        }

        public async Task<User> GetUserWithTags(int user_id)
        {
            var query1 = "SELECT * FROM Users WHERE Id = @user_id";
            var query2 = "SELECT Tags.Id, Tags.Name, Tags.SchoolId, Tags.Type, Tags.Created, Tags.Color FROM Tags " +
                         "JOIN UserTags " +
                         "  ON UserTags.TagId = Tags.Id " +
                         "WHERE UserTags.UserId = @user_id ";
            var query3 = "SELECT Roles.Id, Roles.Name, Roles.Created FROM Roles " +
                         "JOIN UserRoles " +
                         "  ON Roles.Id = UserRoles.RoleId " +
                         "WHERE UserRoles.UserId = @user_id";

            using (var connection = _context.CreateConnection())
            {
                User user = (User) await connection.QuerySingleOrDefaultAsync<User>(query1, new { user_id });
                User detailedUser = new User();
                
                var parameters = new DynamicParameters();
                parameters.Add("user_id", user.Id, DbType.Int32);
                user.Tags = (await connection.QueryAsync<Tag>(query2, parameters)).ToList();
                user.Role = await connection.QueryFirstOrDefaultAsync<Role>(query3, parameters);
                //detailedUser = user;

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUsersBySchoolId(int school_id)
        {
            var query = "SELECT * FROM Users WHERE SchoolId = @school_id";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query, new {school_id});
                return users.ToList();
            }
        }

        public async Task DeleteUserProfilePicUrl(int id)
        {
            var query = "UPDATE Users SET ProfilePictureUrl = null WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task UpdateUserProfilePicUrl(int id, string url)
        {
            var query = "UPDATE Users SET ProfilePictureUrl = @Url WHERE Id = @Id";
            
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { url, id});
            }
        }

        public async Task<IEnumerable<Tag>> GetUserTags(int id)
        {
            var query = "SELECT Tags.id, Tags.name, Tags.schoolid, Tags.type, Tags.created, Tags.color FROM Tags " +
                        "JOIN UserTags "+
                        "ON UserTags.TagID = Tags.ID "+
                        "JOIN Users "+
                        "ON Users.ID = UserTags.UserID "+
                        "WHERE Users.ID = @Id";
            using (var connection = _context.CreateConnection())
            {
                var tags = await connection.QueryAsync<Tag>(query, new { id });
                return tags.ToList();
            }
        }
        /*
        public async Task UpdateUserTags(int id, List<int> TagIdsToAdd, List<int> TagIdsToRemove)
        {
            var query = "INSERT INTO UserTags (UserId, TagId) VALUES ";
            foreach(var tagId in TagIdsToAdd)
            {
                query += "( @Id, " + tagId.ToString() + ")";
            }
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
        */

        public async Task AddUserTag(int id, int tag_id)
        {
            var query = "INSERT INTO UserTags (UserId, TagId) VALUES (@id, @tag_id)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id, tag_id });
            }
        }

        public async Task RemoveUserTag(int id, int tag_id)
        {
            var query = "DELETE FROM UserTags WHERE UserId = @id AND TagId = @tag_id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id, tag_id });
            }
        }


        /*

        public async Task CreateSchool(SchoolForCreationDto school)
        {
            var query = "INSERT INTO Schools (ShortName, LongName, LogoUrl, WebsiteUrl, Address, City, PostalCode, Country) " +
                        "VALUES (@ShortName, @LongName, @LogoUrl, @WebsiteUrl, @Address, @City, @PostalCode, @Country)";

            //var query = "INSERT INTO Schools (ShortName, LongName, LogoUrl, WebsiteUrl, Address, City, PostalCode, Country) " +
              //          "VALUES (@ShortName, @LongName, @LogoUrl, @WebsiteUrl, @Address, @City, @PostalCode, @Country)" +
                //        "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("ShortName", school.ShortName, DbType.String);
            parameters.Add("LongName", school.LongName, DbType.String);
            parameters.Add("LogoUrl", school.LogoUrl, DbType.String);
            parameters.Add("WebsiteUrl", school.WebsiteUrl, DbType.String);
            parameters.Add("Address", school.Address, DbType.String);
            parameters.Add("City", school.City, DbType.String);
            parameters.Add("PostalCode", school.PostalCode, DbType.Int32);
            parameters.Add("Country", school.Country, DbType.String);
            
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                /*
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdSchool = new School
                {
                    Id = id,
                    ShortName = school.ShortName,
                    LongName= school.LongName,
                    LogoUrl = school.LogoUrl,
                    WebsiteUrl = school.WebsiteUrl,
                    Address = school.Address,
                    City = school.City,
                    PostalCode = school.PostalCode,
                    Country = school.Country
                };
                return createdSchool;
                
            }

           
        }

        public async Task UpdateSchool(int id, SchoolForUpdateDto school)
        {
            var query = "UPDATE Schools SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";
            
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("ShortName", school.ShortName, DbType.String);
            parameters.Add("LongName", school.LongName, DbType.String);
            parameters.Add("LogoUrl", school.LogoUrl, DbType.String);
            parameters.Add("WebsiteUrl", school.WebsiteUrl, DbType.String);
            parameters.Add("Address", school.Address, DbType.String);
            parameters.Add("City", school.City, DbType.String);
            parameters.Add("PostalCode", school.PostalCode, DbType.String);
            parameters.Add("Country", school.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteSchool(int id)
        {
            var query = "DELETE FROM Schools WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id } );
            }
        }

        public Task<School> GetSchoolUsersMultipleResults(int id)
        {
            throw new NotImplementedException();
        }

        /*
         * 
         * BACA NEKI ERROR, nije bitno
        public async Task<School> GetSchoolUsersMultipleResults(int id)
        {
            var query = "SELECT * FROM Schools WHERE Id = @Id;" +
                        "SELECT * FROM Users WHERE SchoolId = @Id";
            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var school = await multi.ReadSingleOrDefaultAsync<School>();
                if (school != null)
                {
                    school.Users = (await multi.ReadAsync<School>()).ToList();
                }

                return school;
            }
        }
        */
    }
}

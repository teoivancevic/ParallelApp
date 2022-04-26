using Dapper;
using ParallelApp.Server.Contracts;
using ParallelApp.Server.Models;
using ParallelApp.Shared.Models;
using ParallelApp.Shared.Dto;
using System.Data;

namespace ParallelApp.Server.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly DapperContext _context;
        public SchoolRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<School>> GetSchools()
        {
            var query = "SELECT * FROM Schools";

            using (var connection = _context.CreateConnection())
            {
                var schools = await connection.QueryAsync<School>(query);
                return schools.ToList();
            }
        }

        public async Task<School> GetSchool(int id)
        {
            var query = "SELECT * FROM Schools WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var school = await connection.QuerySingleOrDefaultAsync<School>(query, new { id });
                return school;
            }
        }

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
                */
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

        public async Task<IEnumerable<Tag>> GetSchoolTagsBySchoolId(int id)
        {
            var query = "SELECT Tags.id, Tags.name, Tags.schoolid, Tags.type, Tags.created, Tags.color FROM Tags " +
                        "JOIN Schools " +
                           "ON Schools.ID = Tags.SchoolID " +
                        "WHERE Tags.SchoolID = @Id";
            // AND Tags.Type = 'općenito'

            using (var connection = _context.CreateConnection())
            {
                var tags = await connection.QueryAsync<Tag>(query, new { id });
                return tags.ToList();
            }
        }

        public async Task<Tag> GetSchoolTagByName(int school_id, string name)
        {
            var query = "SELECT * FROM Tags WHERE SchoolID = @school_id AND Name = @name";

            using (var connection = _context.CreateConnection())
            {
                var tag = await connection.QueryFirstOrDefaultAsync<Tag>(query, new { school_id, name });
                return tag;
            }
        }

        public async Task CreateSchoolTag(TagForCreationDto tag) 
        {
            var query = "INSERT INTO Tags (Name, SchoolID, Type, Color) VALUES (@Name, @SchoolID, @Type, @Color)";
            //var query = "INSERT INTO Tags (Name, SchoolID, Type, Color) VALUES ('tagtest', 1, 'općenito', '#00c853')";
            var parameters = new DynamicParameters();
            
            parameters.Add("Name", tag.Name, DbType.String);
            parameters.Add("SchoolID", tag.SchoolId, DbType.Int32);
            parameters.Add("Type", tag.Type, DbType.String);
            parameters.Add("Color", tag.Color, DbType.String);


            using (var connection = _context.CreateConnection())
            {
                //await connection.ExecuteAsync(query, parameters);
                await connection.ExecuteAsync(query, new { Name=tag.Name , SchoolID = tag.SchoolId , Type = tag.Type , Color = tag.Color});
            }
        }

        public async Task UpdateSchoolTag(int tag_id, TagForUpdateDto tag)
        {
            var query = "UPDATE Tags SET Name = @Name, Color = @Color WHERE Id = @Id";
            var parameters = new DynamicParameters();

            parameters.Add("Id", tag_id, DbType.Int32);
            parameters.Add("Name", tag.Name, DbType.String);
            parameters.Add("Color", tag.Color, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeleteSchoolTag(int tag_id)
        {
            var query = "DELETE FROM Tags WHERE ID = @tag_id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { tag_id });
            }

        }

        public async Task<Tag> GetTagById(int id)
        {
            var query = "SELECT Id, Name, Type, SchoolID, Created, Color FROM Tags WHERE ID = @Id";
            using (var connection = _context.CreateConnection())
            {
                var tag = await connection.QuerySingleOrDefaultAsync<Tag>(query, new { id });
                return tag;
            }
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

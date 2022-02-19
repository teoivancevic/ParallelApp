using ParallelApp.Shared.Models;
using ParallelApp.Shared.Dto;

namespace ParallelApp.Server.Contracts
{
    public interface ISchoolRepository
    {
        public Task<IEnumerable<School>> GetSchools();
        public Task<School> GetSchool(int id);
        //public Task<School> CreateSchool(SchoolForCreationDto school);
        public Task CreateSchool(SchoolForCreationDto school);
        public Task UpdateSchool(int id, SchoolForUpdateDto school);
        public Task DeleteSchool(int id);
        public Task<School> GetSchoolUsersMultipleResults(int id);
        public Task<IEnumerable<Tag>> GetSchoolTagsBySchoolId(int id);
    }
}

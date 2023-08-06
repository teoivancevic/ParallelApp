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
        public Task<Tag> GetSchoolTagByName(int school_id, string name);

        public Task CreateSchoolTag(TagForCreationDto tag);
        public Task UpdateSchoolTag(int tag_id, TagForUpdateDto tag);
        public Task DeleteSchoolTag(int tag_id);
        public Task<Tag> GetTagById(int id);
    }
}

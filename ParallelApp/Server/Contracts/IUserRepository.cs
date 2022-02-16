using ParallelApp.Shared.Models;
//using ParallelApp.Shared.Dto;

namespace ParallelApp.Server.Contracts
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(int id);
        public Task<IEnumerable<User>> GetUsers();
        public Task DeleteUserProfilePicUrl(int id);
        public Task UpdateUserProfilePicUrl(int id, string url);
        public Task<IEnumerable<Tag>> GetUserTags(int id);
        //public Task UpdateUserTags(int id, List<int> TagIdsToAdd, List<int> TagIdsToRemove);
        public Task RemoveUserTags(int id, List<int> TagIdsToRemove);
        public Task AddUserTags(int id, List<int> TagIdsToAdd);
        /*
        public Task CreateSchool(SchoolForCreationDto school);
        public Task UpdateSchool(int id, SchoolForUpdateDto school);
        public Task DeleteSchool(int id);
        public Task<School> GetSchoolUsersMultipleResults(int id);
        */
    }
}

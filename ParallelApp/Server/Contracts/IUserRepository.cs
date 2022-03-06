using ParallelApp.Shared.Models;
//using ParallelApp.Shared.Dto;

namespace ParallelApp.Server.Contracts
{
    public interface IUserRepository
    {
        public Task<int> GetUserId(string auth0Id);
        public Task<User> GetUserById(int id);
        public Task<IEnumerable<User>> GetUsers();
        public Task<IEnumerable<User>> GetUsersWithTags(int school_id);
        public Task<User> GetUserWithTags(int user_id);
        public Task<IEnumerable<User>> GetUsersBySchoolId(int school_id); 
        public Task DeleteUserProfilePicUrl(int id);
        public Task UpdateUserProfilePicUrl(int id, string url);
        public Task<IEnumerable<Tag>> GetUserTags(int id);
        //public Task UpdateUserTags(int id, List<int> TagIdsToAdd, List<int> TagIdsToRemove);
        public Task AddUserTag(int id, int tag_id);
        public Task RemoveUserTag(int id, int tag_id);
        
        
        /*
        public Task CreateSchool(SchoolForCreationDto school);
        public Task UpdateSchool(int id, SchoolForUpdateDto school);
        public Task DeleteSchool(int id);
        public Task<School> GetSchoolUsersMultipleResults(int id);
        */
    }
}

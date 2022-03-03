using ParallelApp.Shared.Models;


namespace ParallelApp.Client.ViewModels
{
    public class FeedViewModel
    {
        public int user_id { get; set; }
        public User appUser { get; set; }
        public List<Message> userFeed { get; set; }
        /*
        public async Task<User> GetUserWithTags(int id)
        {
            return await Http.GetFromJsonAsync<User>("api/user/getuserwithtags/" + id.ToString());
            //StateHasChanged();
        }

        public async Task<List<Message>> GetUserFeed(int id)
        {
            return await Http.GetFromJsonAsync<List<Message>>("api/message/getuserfeed/" + id.ToString());
            //userTags = await Http.GetFromJsonAsync<List<Tag>>("api/user/getusertags/" + id.ToString());
        }
        */
    }
}

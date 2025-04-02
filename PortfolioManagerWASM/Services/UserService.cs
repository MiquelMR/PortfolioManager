using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<User> CreateUser(User user)
        {
            var content = JsonConvert.SerializeObject(user);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/users", bodyContent);
            if (response.IsSuccessStatusCode)
            {

                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/users/{UserId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<User> GetUser(int UserId)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/users/{UserId}");
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(content);
                return user;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/users");
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            return users;
        }

        public async Task<User> UpdateUser(int UserId, User user)
        {
            var body = JsonConvert.SerializeObject(user);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");
            var response = await _httpClient.PatchAsync($"{Initialize.UrlBaseApi}api/users/{UserId}", bodyContent);
            if (response.IsSuccessStatusCode)
            {

                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}

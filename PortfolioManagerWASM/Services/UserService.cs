using Blazored.LocalStorage;
using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class UserService(HttpClient httpClient, ILocalStorageService localStorage) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorage = localStorage;
        public User ActiveUser { get; set; } = null;

        public async Task InitializeAsync()
        {
            ActiveUser = await GetActiveUserAsync();
        }

        public async Task<bool> DeleteUser(string Email)
        {
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/users/{Email}");
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

        public async Task<User> GetUserById(int UserId)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/users/by-id/{UserId}");
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

        public async Task<User> GetUserByEmail(string Email)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/users/by-email/{Email}");
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
        private async Task<User> GetActiveUserAsync()
        {
            if (ActiveUser != null) return ActiveUser;

            var activeUserEmail = await _localStorage.GetItemAsync<string>(Initialize.User_Local_Data);

            return string.IsNullOrEmpty(activeUserEmail)
                ? null
                : await GetUserByEmail(activeUserEmail);
        }

        public void CleanActiveUser()
        {
            ActiveUser = null;
        }
    }
}

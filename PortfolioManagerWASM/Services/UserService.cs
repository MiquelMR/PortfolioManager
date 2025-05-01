using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;
using System.Net.Http.Headers;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class UserService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
        public User ActiveUser { get; set; } = new();

        public async Task InitializeAsync()
        {
            ActiveUser = await GetActiveUserAsync();
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
        public async Task<AuthResponse> LoginUser(UserLoginDto userLoginDto)
        {
            var content = JsonConvert.SerializeObject(userLoginDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/users/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                var Token = result["result"]["token"].Value<string>();
                var User = result["result"]["userLoginDto"]["email"].Value<string>();

                await _localStorage.SetItemAsync(Initialize.Token_Local, Token);
                await _localStorage.SetItemAsync(Initialize.User_Local_Data, User);
                ((AuthStateProvider)_authenticationStateProvider).NotifyLoggedUser(Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                return new AuthResponse { IsSuccess = true };
            }
            else
            {
                return new AuthResponse { IsSuccess = false };
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(Initialize.Token_Local);
            await _localStorage.RemoveItemAsync(Initialize.User_Local_Data);
            ((AuthStateProvider)_authenticationStateProvider).NotifyLoggedUser();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> RegisterUser(UserRegisterDto registerUserDto)
        {
            var body = JsonConvert.SerializeObject(registerUserDto);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/users/register", bodyContent);
            if (response.IsSuccessStatusCode)
            {

                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(contentTemp);
                return true;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> DeleteUserAsync()
        {
            var email = ActiveUser.Email;
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/users/{email}");
            if (response.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync(Initialize.Token_Local);
                await _localStorage.RemoveItemAsync(Initialize.User_Local_Data);
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
        public async Task<User> UpdatePublicProfile(UserUpdateDto userUpdateDto)
        {
            userUpdateDto.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList()
                .ForEach(p =>
                {
                    var value = (string)p.GetValue(userUpdateDto);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        p.SetValue(userUpdateDto, null);
                    }
                });
            var body = JsonConvert.SerializeObject(userUpdateDto);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");

            var response = await _httpClient.PatchAsync($"{Initialize.UrlBaseApi}api/users/updatePublicProfile", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(contentTemp);
                await _localStorage.SetItemAsync(Initialize.User_Local_Data, result.Email);
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
            var activeUserEmail = await _localStorage.GetItemAsync<string>(Initialize.User_Local_Data);
            if (activeUserEmail == null || activeUserEmail == string.Empty)
            {
                return new();
            }
            return await GetUserByEmail(activeUserEmail);
        }
    }
}

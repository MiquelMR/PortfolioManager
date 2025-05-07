using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortfolioManagerAPI.Models;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.UserDto;
using PortfolioManagerWASM.Services.IService;
using System.Net.Http.Headers;
using System.Security.Principal;
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
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<User>>(contentTemp);
            var user = responseAPI.Data;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(responseAPI.Message);
                return user;
            }
            return null;
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
                var Token = result["data"]["token"].Value<string>();
                var User = result["data"]["userLoginDto"]["email"].Value<string>();

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

        public async Task<User> RegisterUser(UserRegisterDto registerUserDto)
        {
            var body = JsonConvert.SerializeObject(registerUserDto);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/users/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<User>>(contentTemp);
            var user = responseAPI.Data;

            Console.WriteLine(responseAPI.Message);
            if (response.IsSuccessStatusCode)
            {
                return user;
            }
            else
            {
                Console.WriteLine(responseAPI.Message);
                return user;
            }
        }

        public async Task<bool> DeleteUserAsync()
        {
            var email = ActiveUser.Email;
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/users/{email}");
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<User>>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync(Initialize.Token_Local);
                await _localStorage.RemoveItemAsync(Initialize.User_Local_Data);
                return true;
            }
            else
            {
                Console.WriteLine(responseAPI.Message);
                return false;
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
            var response = await _httpClient.PatchAsync($"{Initialize.UrlBaseApi}api/users", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<User>>(contentTemp);
            var user = responseAPI.Data;

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(Initialize.User_Local_Data, responseAPI.Data.Email);
                return user;
            }
            else
            {
                Console.WriteLine(responseAPI.Message);
                return null;
            }
        }

        private async Task<User> GetActiveUserAsync()
        {
            var activeUserEmail = await _localStorage.GetItemAsync<string>(Initialize.User_Local_Data);
            if (activeUserEmail == null || activeUserEmail == string.Empty)
            {
                await _localStorage.RemoveItemAsync(Initialize.Token_Local);
                await _localStorage.RemoveItemAsync(Initialize.User_Local_Data);
                return null;
            }
            return await GetUserByEmail(activeUserEmail);
        }
    }
}

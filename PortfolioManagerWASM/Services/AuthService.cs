using Blazored.LocalStorage;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<AuthResponse> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var content = JsonConvert.SerializeObject(userRegisterDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/users/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthResponse>(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                return new AuthResponse
                {
                    IsSuccess = true
                };
            }
            else
            {
                return result;
            }
        }

        public async Task<AuthResponse> Login(UserLoginDto userLoginDto)
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
    }
}

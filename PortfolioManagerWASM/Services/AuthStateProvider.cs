using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Helpers;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace PortfolioManagerWASM.Services
{
    public class AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorageService = localStorageService;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>(Initialize.Token_Local);
            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyLoggedUser(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyLoggedUser()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}

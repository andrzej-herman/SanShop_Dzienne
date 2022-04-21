using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SanShop.WebApp.Auth
{
    public class SanShopStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        public SanShopStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            var userId = await _localStorage.GetItemAsync<string>("sanShopUserId");
            var userName = await _localStorage.GetItemAsync<string>("sanShopUserName");
            var token = await _localStorage.GetItemAsync<string>("sanShopToken");
            if (userId != null && userName != null && token != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Authentication, token),
                }, "apiauth_type");
            }
            else
                identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(string userId, string userName, string token)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Authentication, token),
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            _localStorage.RemoveItemAsync("sanShopUserId");
            _localStorage.RemoveItemAsync("sanShopUserName");
            _localStorage.RemoveItemAsync("sanShopToken");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}







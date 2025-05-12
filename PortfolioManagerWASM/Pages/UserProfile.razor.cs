using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.UserDto;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class UserProfile
    {
        [Inject]
        private UserProfileViewModel UserProfileViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public User ActiveUser { get; set; } = new();
        public UserProfileView UserProfileView { get; set; } = UserProfileView.ProfileView;
        public Func<UserUpdateDto, Task> UpdatePublicProfileAsyncDelegate { get; set; }
        public Func<Task> DeleteUserAsyncDelegate { get; set; }

        protected override void OnInitialized()
        {
            UserProfileViewModel.Init();
            ActiveUser = UserProfileViewModel.ActiveUser;

            UpdatePublicProfileAsyncDelegate = async (userUpdateDto) =>
            {
                var user = await UserProfileViewModel.UpdatePublicProfileAsync(userUpdateDto);
                if (user != null)
                    NavigationManager.NavigateTo("/home", true);
            };

            DeleteUserAsyncDelegate = async () =>
            {
                await UserProfileViewModel.DeleteUserAsync();
                NavigationManager.NavigateTo("/login", true);
            };
        }

        public void ToUpdateView()
        {
            UserProfileView = UserProfileView.UpdateView;
        }
    }

    public enum UserProfileView
    {
        ProfileView,
        UpdateView
    }
}
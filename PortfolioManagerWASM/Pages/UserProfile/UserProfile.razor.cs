using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.UserDto;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages.UserProfile
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
                await UserProfileViewModel.UpdatePublicProfileAsyncDelegate.Invoke(userUpdateDto);
                NavigationManager.NavigateTo("/home", true);
            };

            DeleteUserAsyncDelegate = async () =>
            {
                await UserProfileViewModel.DeleteUserAsyncDelegate.Invoke();
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
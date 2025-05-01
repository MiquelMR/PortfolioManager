using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
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
        public Func<UserUpdateDto, Task> UpdateUserAsyncDelegate { get; set; }
        public Func<Task> DeleteUserAsyncDelegate { get; set; }

        protected override void OnInitialized()
        {
            UserProfileViewModel.Init();
            ActiveUser = UserProfileViewModel.ActiveUser;

            UpdateUserAsyncDelegate = async (userUpdateDto) =>
            {
                await UserProfileViewModel.UpdateUserAsyncDelegate.Invoke(userUpdateDto);
                NavigationManager.NavigateTo("/home");
            };

            DeleteUserAsyncDelegate = async () =>
            {
                await UserProfileViewModel.DeleteUserAsyncDelegate.Invoke();
                NavigationManager.NavigateTo("/login");
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
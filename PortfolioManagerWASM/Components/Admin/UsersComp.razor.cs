using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components.Admin
{
    public partial class UsersComp
    {
        // Properties
        [Parameter] public List<User> Users { get; set; }

        // Delegates
        [Parameter] public Action<User> OnDeleteUserDelegate { get; set; }

        private void OnDeleteUser(User user)
        {
            OnDeleteUserDelegate.Invoke(user);
        }



    }
}
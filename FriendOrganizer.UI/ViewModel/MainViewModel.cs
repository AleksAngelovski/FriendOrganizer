using FriendOrganizer.Core.Model;
using FriendOrganizer.UI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public  IFriendDetailViewModel FriendDetailViewModel { get; }

        public INavigationViewModel NavigationViewModel { get; }

        public MainViewModel(INavigationViewModel navigationViewModel, IFriendDetailViewModel friendDetailViewModel)
        {
            this.NavigationViewModel = navigationViewModel;
            this.FriendDetailViewModel = friendDetailViewModel;
        }
        public async Task LoadFriendsAsync()
        {
            await NavigationViewModel.LoadFriendsLookupAsync();
        }
       
    }
}

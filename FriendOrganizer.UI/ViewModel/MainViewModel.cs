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
        private readonly IFriendDataService _friendDataService;
        private Friend _selectedFriend;

        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            this._friendDataService = friendDataService;
        }
        public void LoadFriends()
        {
            var friends = _friendDataService.GetAll();
            //Clear so that we dont have duplicates when we click to load
            Friends.Clear();
            foreach(var friend in friends)
            {
                Friends.Add(friend);
            }
        }
        public ObservableCollection<Friend> Friends { get; set; }


        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set { _selectedFriend = value;
                //OnPropertyChanged(nameof(SelectedFriend)); 

                base.OnPropertyChanged();
            }
        }
    
       
    }
}

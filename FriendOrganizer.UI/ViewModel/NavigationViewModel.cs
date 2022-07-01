using FriendOrganizer.Core.Dtos;
using FriendOrganizer.Core.Model;
using FriendOrganizer.Infra.DataAccess.DataAccess;
using FriendOrganizer.UI.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel :ViewModelBase, INavigationViewModel
    {
        private readonly IFriendLookupDataService _friendLookupDataService;
        private IEventAggregator _eventAggregator { get; }

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            this._friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<LookupItemDto>();
        }

        public async Task LoadFriendsLookupAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(item);
            }

        }

        public ObservableCollection<LookupItemDto> Friends { get; }

        private LookupItemDto _selectedFriend;

        public LookupItemDto SelectedFriend
        {
            get { return _selectedFriend; }
            set { _selectedFriend = value;
                OnPropertyChanged();
                if(_selectedFriend!= null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                        .Publish(_selectedFriend.Id);
                }
            }
        }


    }
}

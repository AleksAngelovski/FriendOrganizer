using FriendOrganizer.Core.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Events;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private readonly IFriendDataService _friendDataService;
        private readonly IEventAggregator _eventAggregator;

        public FriendDetailViewModel(IFriendDataService friendDataService, IEventAggregator eventAggregator)
        {
            this._friendDataService = friendDataService;
            this._eventAggregator = eventAggregator;
            //Subscribe when this event gets fired and use this method
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);

        }

        private async void OnSaveExecute()
        {
            await _friendDataService.SaveAsync(Friend);
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(
                new AfterFriendSavedEventArgs
                {
                    Id= Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                }); ;
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }


        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadFriendAsync(friendId);
        }

        public async Task LoadFriendAsync(int friendId)
        {
            Friend = await _friendDataService.GetByIdAsync(friendId);
        }
        private Friend _friend;

        public Friend Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

    }
}

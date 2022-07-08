using FriendOrganizer.Core.Model;
using FriendOrganizer.Infra.DataAccess.DataAccess.Repositories;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Services;
using FriendOrganizer.UI.Wrapper;
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
        private readonly IFriendRepository _friendRepository;
        private readonly IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private FriendWrapper _friend;
        private bool _hasChanges;

        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            this._friendRepository = friendRepository;
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;
            //Subscribe when this event gets fired and use this method
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

        }

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the friend {Friend.FirstName} {Friend.LastName} ?", "Delete?");
            if(result == MessageDialogResult.Cancel)
            {
                return;
            }
            _friendRepository.Remove(Friend.Model);
            await _friendRepository.SaveAsync();
            _eventAggregator.GetEvent<AfterFriendDeltedEvent>().Publish(Friend.Id);
        }

        public FriendWrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                //if its different
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private async void OnSaveExecute()
        {
            await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(
                new AfterFriendSavedEventArgs
                {
                    Id= Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                }); ;
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        public async Task LoadFriendAsync(int? friendId)
        {

            var friend = friendId.HasValue ? 
                await _friendRepository.GetByIdAsync(friendId.GetValueOrDefault()) 
                : CreateNewFriend(); 
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if(HasChanges == false)
                {
                    HasChanges = _friendRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if(Friend.Id == 0)
            {
                //Trick to trigger validaiton(calls the set method)
                Friend.FirstName = "";
                Friend.LastName = "";
                Friend.Email = "";
            }
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _friendRepository.Add(friend);
            return friend;
        }


        public ICommand DeleteCommand { get; private set; }


    }
}

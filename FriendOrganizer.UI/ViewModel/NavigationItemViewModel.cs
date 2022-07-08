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
    public class NavigationItemViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayMember;
        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id=id;
            DisplayMember=displayMember;
            _eventAggregator = eventAggregator;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
        }



        public int Id { get; }
        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFriendDetailViewCommand { get; }

        private void OnOpenFriendDetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                            .Publish(Id);
        }
    }
}

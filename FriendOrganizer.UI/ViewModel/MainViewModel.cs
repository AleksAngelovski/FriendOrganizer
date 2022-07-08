using FriendOrganizer.Core.Model;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Adress> Adresses { get; set; }
    }

    public class Adress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
    public class MainViewModel : ViewModelBase
    {
        private  Func<IFriendDetailViewModel> _friendDetailViewModelCreator { get; }

        private IFriendDetailViewModel _friendDetailViewModel;

        public IFriendDetailViewModel FriendDetailViewModel 
        {
            get { return _friendDetailViewModel; }
            set { _friendDetailViewModel = value;
                OnPropertyChanged();
            }
            
        }


        public INavigationViewModel NavigationViewModel { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
 
        private ObservableCollection<Person> _people { get; set; } = new ObservableCollection<Person>
        {
            new Person{FirstName="Aleks", LastName="Aleksovski", Adresses=new List<Adress>(){ 
                new Adress() { Street = "Teofan Eko", ZipCode =1300, City= "Kumanovo"},
                new Adress() { Street = "Adress2 ", ZipCode =1300, City= "Stavanger"}} },
            new Person{FirstName="ASDSAD", LastName= "adsadsa", Adresses=new List<Adress>(){
                new Adress() { Street = "Address1", ZipCode =4600, City= "Skopje"},
                new Adress() { Street = "Oslo ", ZipCode =5600, City= "Stavanger"}} },
        };
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { _people = value; }
        }

        private Person _selectedPerson = new Person() { FirstName = "Select Person", LastName= "Select Person"};
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public Person SelectedPerson
        {
            get { 
                return _selectedPerson; }
            set { _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public void LoadPageOne()
        {
            FirstName = "CLicked";
            LastName = "Clicked";
        }


        public ICommand CreateNewFriendCommand { get; }
        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this.NavigationViewModel = navigationViewModel;
            this._friendDetailViewModelCreator = friendDetailViewModelCreator;
            this._eventAggregator = eventAggregator;
            this._messageDialogService = messageDialogService;
            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
            _eventAggregator.GetEvent<AfterFriendDeltedEvent>().Subscribe(AfterFriendDeleted);

           
        }



        private async void OnOpenFriendDetailView(int? friendId)
        {
            if(FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You have made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadFriendAsync(friendId);
        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }
        private void AfterFriendDeleted(int friendId)
        {
            FriendDetailViewModel = null;
        }
        public async Task LoadFriendsAsync()
        {
            await NavigationViewModel.LoadFriendsLookupAsync();
        }
       
    }
}

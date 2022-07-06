using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Notifies the View about a change in the property
        /// </summary>
        /// <param name="propertyName">the name of the property that changed.
        ///     The attribute [CallerMemberName] makes it possible that the field propertyName will be automatically filled in by the caller property. 
        ///     (in this case nameof(SelectedFriend) will be called automatically)
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //check if property changed event (if not null something changed)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

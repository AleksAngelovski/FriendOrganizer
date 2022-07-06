using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public interface IFriendDetailViewModel
    {
        Task LoadFriendAsync(int? friendId);

        bool HasChanges { get; }
    }
}
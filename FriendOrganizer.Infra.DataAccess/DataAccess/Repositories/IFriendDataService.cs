using FriendOrganizer.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.Infra.DataAccess.DataAccess.Repositories
{
    public interface IFriendDataService
    {
        Task<Friend> GetByIdAsync(int friendId);

        Task SaveAsync(Friend friend);
    }
}
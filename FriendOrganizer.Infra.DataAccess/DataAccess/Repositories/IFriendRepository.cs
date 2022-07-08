using FriendOrganizer.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.Infra.DataAccess.DataAccess.Repositories
{
    public interface IFriendRepository
    {
        Task<Friend> GetByIdAsync(int friendId);

        Task SaveAsync();

        bool HasChanges();
        void Add(Friend friend);
        void Remove(Friend model);
    }
}
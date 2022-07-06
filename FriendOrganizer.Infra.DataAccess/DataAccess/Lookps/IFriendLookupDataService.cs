using FriendOrganizer.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.Infra.DataAccess.DataAccess.Lookps
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItemDto>> GetFriendLookupAsync();
    }
}
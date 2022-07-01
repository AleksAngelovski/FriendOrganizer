using FriendOrganizer.Core.Model;
using FriendOrganizer.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private readonly FriendOrganizerDbContext _friendOrganizerDbContext;

        public FriendDataService(FriendOrganizerDbContext friendOrganizerDbContext)
        {
            this._friendOrganizerDbContext = friendOrganizerDbContext;
        }
        public async Task<Friend> GetByIdAsync(int friendId)
        {
            return await _friendOrganizerDbContext.Friends.AsNoTracking().SingleAsync(f=>f.Id == friendId);
        }
    }
}

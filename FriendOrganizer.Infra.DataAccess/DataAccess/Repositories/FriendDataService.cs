using FriendOrganizer.Core.Model;
using FriendOrganizer.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.Infra.DataAccess.DataAccess.Repositories
{
    public class FriendDataService : IFriendDataService
    {
        private readonly FriendOrganizerDbContext _friendOrganizerDbContext;

        public FriendDataService(FriendOrganizerDbContext friendOrganizerDbContext)
        {
            _friendOrganizerDbContext = friendOrganizerDbContext;
        }
        public async Task<Friend> GetByIdAsync(int friendId)
        {
            return await _friendOrganizerDbContext.Friends.SingleAsync(f => f.Id == friendId);
        }

        public async Task SaveAsync(Friend friend)
        {
            //We dont need this cause we removed asnotracking.
            //_friendOrganizerDbContext.Friends.Attach(friend);
            //_friendOrganizerDbContext.Entry(friend).State = EntityState.Modified;
            await _friendOrganizerDbContext.SaveChangesAsync();
        }
    }
}

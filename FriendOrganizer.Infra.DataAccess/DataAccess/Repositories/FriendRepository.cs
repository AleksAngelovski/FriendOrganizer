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
    public class FriendRepository : IFriendRepository
    {
        private readonly FriendOrganizerDbContext _friendOrganizerDbContext;

        public FriendRepository(FriendOrganizerDbContext friendOrganizerDbContext)
        {
            _friendOrganizerDbContext = friendOrganizerDbContext;
        }

        public void Add(Friend friend)
        {
            _friendOrganizerDbContext.Friends.Add(friend);
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            return await _friendOrganizerDbContext.Friends.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _friendOrganizerDbContext.ChangeTracker.HasChanges();
        }

        public void Remove(Friend model)
        {
            _friendOrganizerDbContext.Friends.Remove(model);
        }

        public async Task SaveAsync()
        {
            //We dont need this cause we removed asnotracking.
            //_friendOrganizerDbContext.Friends.Attach(friend);
            //_friendOrganizerDbContext.Entry(friend).State = EntityState.Modified;
            await _friendOrganizerDbContext.SaveChangesAsync();
        }
    }
}

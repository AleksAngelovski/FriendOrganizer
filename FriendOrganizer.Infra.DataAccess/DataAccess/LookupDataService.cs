using FriendOrganizer.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.Infra.DataAccess.DataAccess
{
    public class LookupDataService : IFriendLookupDataService
    {
        private readonly FriendOrganizerDbContext _friendOrganizerDbContext;

        public LookupDataService(FriendOrganizerDbContext friendOrganizerDbContext)
        {
            this._friendOrganizerDbContext = friendOrganizerDbContext;
        }

        public async Task<IEnumerable<LookupItemDto>> GetFriendLookupAsync()
        {
            return await _friendOrganizerDbContext.Friends.AsNoTracking().Select(f =>
            new LookupItemDto
            {
                Id = f.Id,
                DisplayMember = f.FirstName + " " + f.LastName
            }).ToListAsync();
        }
    }
}

﻿using FriendOrganizer.Core.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data
{
    public interface IFriendDataService
    {
        IEnumerable<Friend> GetAll();
    }
}
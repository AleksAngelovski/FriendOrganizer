using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.Core.Model
{
    public class Friend
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Models
{
    // Models/Member.cs
    public class Member
    {
        public int MemberId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // stored hash
        public string Role { get; set; } // "Admin" or "Member"
    }

}

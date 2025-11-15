using CityStore.Data;
using CityStore.Models;
using CityStore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Controllers
{
    // Controllers/AuthController.cs
    public class AuthController
    {
        private MemberRepo _members = new MemberRepo();

        public Member Login(string username, string password)
        {
            var user = _members.GetByUsername(username);
            if (user == null) return null;
            if (Security.VerifyPassword(password, user.PasswordHash)) return user;
            return null;
        }

        public bool Register(string username, string rawPassword, string role = "Member")
        {
            var m = new Member { Username = username, Role = role };
            return _members.Create(m, rawPassword);
        }
    }

}

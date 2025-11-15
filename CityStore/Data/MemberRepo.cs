using CityStore.Models;
using CityStore.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Data
{

    public class MemberRepo
    {
        public Member GetByUsername(string username)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT MemberId, Username, PasswordHash, Role FROM Members WHERE Username = @u", conn);
            cmd.Parameters.AddWithValue("@u", username);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new Member
                {
                    MemberId = (int)rdr["MemberId"],
                    Username = (string)rdr["Username"],
                    PasswordHash = (string)rdr["PasswordHash"],
                    Role = (string)rdr["Role"]
                };
            }
            return null;
        }

        public bool Create(Member m, string rawPassword)
        {
            // ensure unique username
            if (GetByUsername(m.Username) != null) return false;
            var hash = Security.HashPassword(rawPassword);
            using var conn = Db.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("INSERT INTO Members (Username, PasswordHash, Role) VALUES (@u,@p,@r)", conn);
            cmd.Parameters.AddWithValue("@u", m.Username);
            cmd.Parameters.AddWithValue("@p", hash);
            cmd.Parameters.AddWithValue("@r", m.Role);
            return cmd.ExecuteNonQuery() == 1;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Utils/Security.cs
using System.Security.Cryptography;


namespace CityStore.Utils
{


    /*public static class Security
    {
        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            foreach (var b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
            // Temporary: allow "temp" as password for testing
           
        }
    }*/

    public static class Security
    {
        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            foreach (var b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        } // Username: admin
        //Password: password123

        public static bool VerifyPassword(string password, string hash)
        {
            var computedHash = HashPassword(password);
            Console.WriteLine($"Comparing: '{computedHash}' vs '{hash}'");
            return computedHash == hash;
        }
    }

}

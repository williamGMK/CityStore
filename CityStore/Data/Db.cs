using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Data
{
    public static class Db
    {
        // Update this to your local connection string (LocalDB or SQL Server)
        public static string ConnectionString =>
            "Server=DESKTOP-M8DJOCM;Database=CityStore;Trusted_Connection=True;MultipleActiveResultSets=True;";

        public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}

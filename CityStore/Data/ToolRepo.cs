using CityStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Data
{
    public class ToolRepo
    {
        public IEnumerable<Tool> GetAll()
        {
            var list = new List<Tool>();
            using var conn = Db.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT ToolId, Category, Condition, IsBorrowed FROM Tools ORDER BY Category, ToolId", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new Tool
                {
                    ToolId = (int)rdr["ToolId"],
                    Category = (string)rdr["Category"],
                    Condition = (string)rdr["Condition"],
                    IsBorrowed = (bool)rdr["IsBorrowed"]
                });
            }
            return list;
        }

        public Tool GetById(int id) { /* similar pattern */ throw new NotImplementedException(); }
        public int Create(Tool t) { /* insert and return id */ throw new NotImplementedException(); }
        public bool Update(Tool t) { /* update category/condition only */ throw new NotImplementedException(); }
        public bool Delete(int id) { /* delete only if IsBorrowed = 0 */ throw new NotImplementedException(); }
        public bool SetBorrowed(int toolId, bool isBorrowed) { /* update IsBorrowed */ throw new NotImplementedException(); }
    }
}

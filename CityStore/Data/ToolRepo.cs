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
        // =============================
        // GET ALL TOOLS
        // =============================
        public IEnumerable<Tool> GetAll()
        {
            var list = new List<Tool>();

            using var conn = Db.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand(
                "SELECT ToolId, Category, Condition, IsBorrowed FROM Tools ORDER BY Category, ToolId",
                conn);

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

        // =============================
        // GET BY ID
        // =============================
        public Tool GetById(int id)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand(
                "SELECT ToolId, Category, Condition, IsBorrowed FROM Tools WHERE ToolId = @id",
                conn);

            cmd.Parameters.AddWithValue("@id", id);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new Tool
                {
                    ToolId = (int)rdr["ToolId"],
                    Category = (string)rdr["Category"],
                    Condition = (string)rdr["Condition"],
                    IsBorrowed = (bool)rdr["IsBorrowed"]
                };
            }

            return null;
        }

        // =============================
        // CREATE NEW TOOL
        // =============================
        public int Create(Tool t)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand(
                "INSERT INTO Tools (Category, Condition, IsBorrowed) OUTPUT INSERTED.ToolId VALUES (@cat, @cond, 0)",
                conn);

            cmd.Parameters.AddWithValue("@cat", t.Category);
            cmd.Parameters.AddWithValue("@cond", t.Condition);

            int newId = (int)cmd.ExecuteScalar();
            return newId;
        }

        // =============================
        // UPDATE TOOL
        // =============================
        public bool Update(Tool t)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand(
                "UPDATE Tools SET Category = @cat, Condition = @cond WHERE ToolId = @id",
                conn);

            cmd.Parameters.AddWithValue("@id", t.ToolId);
            cmd.Parameters.AddWithValue("@cat", t.Category);
            cmd.Parameters.AddWithValue("@cond", t.Condition);

            return cmd.ExecuteNonQuery() == 1;
        }

        // =============================
        // DELETE TOOL (ONLY IF NOT BORROWED)
        // =============================
        public bool Delete(int id)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            // Delete only if IsBorrowed = 0
            using var cmd = new SqlCommand(
                "DELETE FROM Tools WHERE ToolId = @id AND IsBorrowed = 0",
                conn);

            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() == 1;
        }

        // =============================
        // SET BORROWED TRUE/FALSE
        // =============================
        public bool SetBorrowed(int toolId, bool isBorrowed)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand(
                "UPDATE Tools SET IsBorrowed = @b WHERE ToolId = @id",
                conn);

            cmd.Parameters.AddWithValue("@id", toolId);
            cmd.Parameters.AddWithValue("@b", isBorrowed);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}

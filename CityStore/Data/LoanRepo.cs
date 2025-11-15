using CityStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Data
{
    public class LoanRepo
    {
        public bool CreateLoan(Loan loan)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try
            {
                // ensure tool not borrowed
                using var check = new SqlCommand("SELECT IsBorrowed FROM Tools WHERE ToolId = @tid", conn, tran);
                check.Parameters.AddWithValue("@tid", loan.ToolId);
                var isBorrowed = (bool)check.ExecuteScalar();
                if (isBorrowed) { tran.Rollback(); return false; }

                using var ins = new SqlCommand("INSERT INTO Loans (MemberId,ToolId,BorrowDate) VALUES (@m,@t,@b); SELECT SCOPE_IDENTITY()", conn, tran);
                ins.Parameters.AddWithValue("@m", loan.MemberId);
                ins.Parameters.AddWithValue("@t", loan.ToolId);
                ins.Parameters.AddWithValue("@b", loan.BorrowDate.Date);
                var newId = Convert.ToInt32(ins.ExecuteScalar());

                using var upd = new SqlCommand("UPDATE Tools SET IsBorrowed = 1 WHERE ToolId = @tid", conn, tran);
                upd.Parameters.AddWithValue("@tid", loan.ToolId);
                upd.ExecuteNonQuery();

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public bool ReturnLoan(int loanId, DateTime returnDate)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try
            {
                using var get = new SqlCommand("SELECT ToolId, BorrowDate, ReturnDate FROM Loans WHERE LoanId = @id", conn, tran);
                get.Parameters.AddWithValue("@id", loanId);
                using var rdr = get.ExecuteReader();
                if (!rdr.Read()) { tran.Rollback(); return false; }
                var toolId = (int)rdr["ToolId"];
                var borrowDate = (DateTime)rdr["BorrowDate"];
                rdr.Close();

                if (returnDate.Date < borrowDate.Date) { tran.Rollback(); return false; }

                using var updLoan = new SqlCommand("UPDATE Loans SET ReturnDate = @ret WHERE LoanId = @id", conn, tran);
                updLoan.Parameters.AddWithValue("@ret", returnDate.Date);
                updLoan.Parameters.AddWithValue("@id", loanId);
                updLoan.ExecuteNonQuery();

                using var updTool = new SqlCommand("UPDATE Tools SET IsBorrowed = 0 WHERE ToolId = @tid", conn, tran);
                updTool.Parameters.AddWithValue("@tid", toolId);
                updTool.ExecuteNonQuery();

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public IEnumerable<Loan> GetLoansForMember(int memberId) { /* implement SELECT */ throw new NotImplementedException(); }
        public IEnumerable<Loan> GetAllLoans() { /* admin view */ throw new NotImplementedException(); }
    }
}

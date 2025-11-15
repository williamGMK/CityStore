using CityStore.Data;
using CityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityStore.Controllers
{
    // Controllers/LoanController.cs
    public class LoanController
    {
        private LoanRepo repo = new LoanRepo();

        public bool Borrow(int memberId, int toolId, DateTime borrowDate)
        {
            var loan = new Loan { MemberId = memberId, ToolId = toolId, BorrowDate = borrowDate };
            return repo.CreateLoan(loan);
        }

        public bool Return(int loanId, DateTime returnDate)
        {
            return repo.ReturnLoan(loanId, returnDate);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Domain;

namespace MDLibrary.Application.Interfaces
{
    public interface ILoanService
    {
        public void LoanOutBook(Loan loan);
        public void ReturnBook(int loanID);
        public IList<BookCopy> ShowAllBooksOnLoan();
        public IList<Loan> ShowAllBooksLoanedByMember(int memberID);
        public IList<BookCopy> ShowAllBooksNotOnLoan();

        public IList<Loan> GetAllLoans();
        
    }
}

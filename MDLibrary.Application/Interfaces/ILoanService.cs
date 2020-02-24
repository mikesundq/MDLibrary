using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Domain;

namespace MDLibrary.Application.Interfaces
{
    public interface ILoanService
    {
        public void LoanOutBook(Loan loan);
        public void ReturnOneBook(int bookCopyID);
        public IList<BookCopy> ShowAllBooksOnLoan();
        public IList<Loan> ShowAllLoansByMember(int memberID);
        public IList<BookCopy> ShowAllBooksNotOnLoan();
        public IList<Loan> GetAllLoans();
        public Loan GetLoanById(int id);
        public void ReturnAllBooks(int loanID);
        public int CalculateLateFee(DateTime dateToReturnBook);
        List<BookCopy> GetAndReturnBookCopiesById(int id);
    }
}

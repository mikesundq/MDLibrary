using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;

namespace MDLibrary.Infrastructure.Service
{
    public class LoanService : ILoanService
    {
        public List<Loan> Loans = new List<Loan>();

        public void LoanOutBook(Loan loan)
        {
            Loans.Add(loan);
        }

        public void ReturnBook(int loanID)
        {
            Loans.RemoveAll(l => l.ID == loanID);
        }

        public IList<int> ShowAllBooksLoanedByMember(int memberID)
        {
            //Get all loans from this member
            List<Loan> listOfLoanWithCorrectBooks = Loans.FindAll(l => l.MemberID == memberID);

            //Get all bookIDs from the loan list above
            List<int> listOfBookIDsToReturn = new List<int>();
            foreach(var loan in listOfLoanWithCorrectBooks)
            {
                listOfBookIDsToReturn.Add(loan.BookID);
            }

            //Return list of IDs
            return listOfBookIDsToReturn;
        }

        public IList<Book> ShowAllBooksNotOnLoan()
        {
            throw new NotImplementedException();
        }

        public IList<Book> ShowAllBooksOnLoan()
        {
            throw new NotImplementedException();
        }
    }
}

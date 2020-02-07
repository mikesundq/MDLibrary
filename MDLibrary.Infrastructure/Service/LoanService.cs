using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MDLibrary.Infrastructure.Service
{
    public class LoanService : ILoanService
    {
       // public List<Loan> Loans = new List<Loan>();

        private readonly ApplicationDbContext context;
        public LoanService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void LoanOutBook(Loan loan)
        {
            context.Add(loan);
            context.SaveChanges();
        }

        public void ReturnBook(int loanID)
        {
            var loanToRemove = context.Loan.Find(loanID);
            context.Remove(loanToRemove);
            context.SaveChanges();
        }

        public IList<Loan> ShowAllBooksLoanedByMember(int memberID)
        {
            var bookLoans = context.Loan
                .Where(x => x.MemberID == memberID).ToList();

            return bookLoans;
            /*  //Get all loans from this member
              List<Loan> listOfLoanWithCorrectBooks = context.Loan //.FindAll(l => l.MemberID == memberID);

              //Get all bookIDs from the loan list above
              List<int> listOfBookIDsToReturn = new List<int>();
              foreach(var loan in listOfLoanWithCorrectBooks)
              {
                  listOfBookIDsToReturn.Add(loan.BookCopyID);
              }

              //Return list of IDs
              return listOfBookIDsToReturn; 
            return null;*/
        }

        public IList<BookCopy> ShowAllBooksNotOnLoan()
        {
            throw new NotImplementedException();
        }

        public IList<BookCopy> ShowAllBooksOnLoan()
        {
            throw new NotImplementedException();
        }
    }
}

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

        public IList<Loan> GetAllLoans()
        {
            //return context.Loan.Include(b => b.BookCopy).ToList();
            return context.Loan.ToList();
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

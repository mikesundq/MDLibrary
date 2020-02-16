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
            /*  return context.Loan
                  .Include(l => l.BookCopies)
                  .Include(l => l.Member)
                  .ToList(); */
            return context.Loan.ToList();
        }

        public Loan GetLoanById(int id)
        {
            return context.Loan
                .Include(l => l.BookCopies)
                .FirstOrDefault(l => l.ID == id);
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

        public IList<Loan> ShowAllLoansByMember(int memberID)
        {
            var bookLoans = context.Loan
                //  .Include(x => x.Member)
                //  .Include(x => x.BookCopies)
                .Where(x => x.MemberID == memberID).ToList();

            return bookLoans;
        }

        public IList<BookCopy> GetBookCopiesFromLoan(int loanID)
        {
            return context.BookCopy
                .Where(bc => bc.LoanID == loanID).ToList();
        }

        public IList<BookCopy> ShowAllBooksNotOnLoan()
        {
            throw new NotImplementedException();
        }

        public IList<BookCopy> GetAllBooksOnLoan()
        {
            return context.BookCopy
                .Where(bc => bc.LoanID != null)
                .ToList();
        }

        public (IList<BookDetails>, IList<Loan>) GetLoanAndDetails(int id)
        {
            var loan = context.Loan
                .Where(l => l.ID == id).ToList();

            var bookDetails = context.BookDetails
                .Where(bc => bc. == id).ToList();

        }
    }
}

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
            return context.Loan
                .Include(l => l.LoanBooks)
                .ThenInclude(l => l.BookCopy)
                .ThenInclude(l =>  l.BookDetails)
                .Include(l => l.Member)
                .ToList();
        }

        public Loan GetLoanById(int id)
        {
            return context.Loan
                .Include(l => l.LoanBooks)
                .ThenInclude(l => l.BookCopy)
                .ThenInclude(l => l.BookDetails)
                .Include(l => l.Member)
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
              .Include(l => l.LoanBooks)
              .ThenInclude(l => l.BookCopy)
              .ThenInclude(l => l.BookDetails)
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
    }
}

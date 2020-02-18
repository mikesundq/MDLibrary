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
                .Include(l => l.MemberID)
                .ToList();
        }

        public Loan GetLoanById(int id)
        {
            return context.Loan
                .Include(l => l.LoanBooks)
                .ThenInclude(l => l.BookCopy)
                .ThenInclude(l => l.BookDetails)
                .Include(l => l.MemberID)
                .FirstOrDefault(l => l.ID == id);
        }

        public void LoanOutBook(Loan loan)
        {
            context.Add(loan);
            context.SaveChanges();
        }

        public void ReturnOneBook(int bookCopyID)
        {
            var loanBookToReturn = context.LoanBook.FirstOrDefault(l => l.BookCopyID == bookCopyID);
            context.Remove(loanBookToReturn);
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

        public IList<BookCopy> ShowAllBooksNotOnLoan()
        {
            throw new NotImplementedException();
        }

        public (IList<BookDetails>, IList<Loan>) GetLoanAndDetails(int id)
        {
            /*var loan = context.Loan
                .Where(l => l.ID == id).ToList();

            var bookDetailsID = loan.

            var bookDetails = context.BookDetails
                .Where(bc => bc == id).ToList();*/

            throw new NotImplementedException();
        }

        public IList<BookCopy> ShowAllBooksOnLoan()
        {
            throw new NotImplementedException();
        }
    }
}

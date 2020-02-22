using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

/*
  
            @if (item.IsReturned == 0)
            {
                <style>
                    color:"Blue"
                </style>
            }
            else if (item.IsReturned == 0 && item.TimeToReturnBook < DateTime.Today)
            {
                <style>
                    color:"Red"
                </style>
            }
            else
            {
                <style>
                    color:"Black"
                </style>
            }
            */
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

            //Get loan id
            context.Entry(loan).GetDatabaseValues();

            //foreach bookcopy add LoanBook
            foreach (var book in loan.BookCopies)
            {
                var loanBookToAdd = new LoanBook();
                loanBookToAdd.BookCopyID = book.ID;
                loanBookToAdd.LoanID = loan.ID;
                context.Add(loanBookToAdd);
            };
            //Save changes
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
            var books = context.BookCopy
                .Include(b => b.BookDetails)
                .ThenInclude(bd => bd.Author)
                .ToList();
            var loans = ShowAllBooksOnLoan();
            //var loans = context.LoanBook
            //   .Include(l => l.BookCopy)
            //   .Select(l => l.BookCopy)
            //   .ToList();
            foreach (var loan in loans)
                books.Remove(loan);

            return books;

        }

        public IList<BookCopy> ShowAllBooksOnLoan()
        {
            return context.LoanBook
               .Include(l => l.BookCopy)
               .ThenInclude(b => b.BookDetails)
               .ThenInclude(bd => bd.Author)
               .Select(l => l.BookCopy)
               .ToList();
        }

        public void ReturnAllBooks(int loanID)
        {

            foreach (var item in context.LoanBook.Where(l => l.LoanID == loanID))
            {
                context.LoanBook.Remove(item);
            }
            var loanReturned = GetLoanById(loanID);
            loanReturned.IsReturned = 1;
            context.Update(loanReturned);
            context.SaveChanges();

        }

        public int CalculateLateFee(DateTime dateToReturnBook)
        {
            int lateFee = 0;

            //dateToReturnBook: 2020-02-10. Todays date: 2020-02-20. Today - dateToReturnBook = 10 dgr
            TimeSpan nrOfDaysLate = DateTime.Today - dateToReturnBook;

            lateFee = 12 * nrOfDaysLate.Days;
            
            return lateFee;
        }
    }
}

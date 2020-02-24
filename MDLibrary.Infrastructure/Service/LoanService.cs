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
                .OrderBy(l => l.IsReturned)
                .ThenBy(l => l.TimeToReturnBook)
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

            //Create LoanBook and add bookID and loanID before adding to database.
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
            if(loanBookToReturn != null)
            {
                context.Remove(loanBookToReturn);
                var loanToCheck = GetLoanById(loanBookToReturn.LoanID);
                if (loanToCheck.LoanBooks.Count <= 1)
                {
                    loanToCheck.IsReturned = 1;
                    context.Update(loanToCheck);
                }
                context.SaveChanges();
            }
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

            foreach (var loan in loans)
                books.Remove(loan);

            return books;

        }

        public IList<LoanBook> GetAllBooksOnLoan()
        {
            return context.LoanBook
                .Include(l => l.BookCopy)
                .ThenInclude(bc => bc.BookDetails)
                .ThenInclude(bd => bd.Author)
                .Include(l => l.Loan)
                .ThenInclude(l => l.Member)
                .ToList();
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

            //Example: dateToReturnBook: 2020-02-10. Todays date: 2020-02-20. Today - dateToReturnBook = 10 dgr
            TimeSpan nrOfDaysLate = DateTime.Today - dateToReturnBook;

            lateFee = 12 * nrOfDaysLate.Days;
            
            return lateFee;
        }

        public List<BookCopy> GetAndReturnBookCopiesById(int id)
        {
            var books = context.BookCopy
                .Where(bc => bc.BookDetailsID == id).ToList();

            foreach(var item in books)
            {
                ReturnOneBook(item.ID);
            }

            return books;
        }
    }
}

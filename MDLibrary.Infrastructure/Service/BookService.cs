using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MDLibrary.Infrastructure.Service
{
    public class BookServices : IBookServices
    {
        private readonly ApplicationDbContext context;

        public BookServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddMoreCopiesOfBook(BookCopy book)
        {
            context.BookCopy.Add(book);
            context.SaveChanges();
        }

        public void AddNewBookDetails(BookDetails bookdetails)
        {
            context.Add(bookdetails);
            context.SaveChanges();
        }

        public void DeleteBookDetailsByID(int id)
        {
            var bookDetails = context.BookDetails.Find(id);
            context.BookDetails.Remove(bookDetails);
            context.SaveChanges();
        }

        public IList<BookDetails> ShowAllBookDetails()
        {
            return context.BookDetails.Include(x => x.Author).OrderBy(x => x.Titel).ToList();
        }

        public IList<BookDetails> ShowAllBooksByAuthor(int id)
        {
            return context.BookDetails.Where(b => b.AuthorID == id).ToList();     
        } 

        public int ShowNumberOfBooks(int id)
        {
            return context.BookCopy.Where(b => b.BookDetailsID == id).ToList().Count;
        }

        public BookDetails GetBookDetailsById(int id)
        {    
            return context.BookDetails
                .Include(b => b.Author)
                .Include(b => b.BookCopies)
                .FirstOrDefault(b => b.ID == id);     
        }

        public void UpdateBookDetails(BookDetails bookDetails)
        {
            context.Update(bookDetails);
            context.SaveChanges();
        }

        public BookCopy GetBookCopyById(int id)
        {
            return context.BookCopy.Find(id);
        }

        public IList<BookCopy> GetBookCopiesById(int[] ids)
        {
            var checkedBooks = new List<BookCopy>();

            foreach (var id in ids)
            {
                var bookCopyToAdd = context.BookCopy.Find(id);

                checkedBooks.Add(bookCopyToAdd);
            }

            return checkedBooks;
        }

        public bool CanRemoveBookDetails(int id)
        {
            var bookDetails = context.BookDetails.Include(bd => bd.BookCopies)
                .FirstOrDefault(bc => bc.ID == id);

            return bookDetails.BookCopies.ToList().Count < 1;
        }

        public void DeleteBookCopiesByID(List<BookCopy> bookCopies)
        {
            context.BookCopy
                .RemoveRange(bookCopies);
            context.SaveChanges();
        }

        public bool CanRemoveBookCopy(int id)
        {
            //Returns true if there is nothing returned
            return !context.LoanBook.Any(lb => lb.BookCopyID == id);
        }
    }
}

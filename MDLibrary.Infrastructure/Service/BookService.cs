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

        //public List<BookDetails> ListOfBookDetails = new List<BookDetails>();
        //public List<BookCopy> ListOfBooks = new List<BookCopy>();

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

        public IList<BookDetails> ShowAllAvailableBooks()
        {
            throw new NotImplementedException();
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
            //check how this works..
            //return context.BookDetails.Include(b => b.BookCopies).
            //context.BookDetails.Include    
            return context.BookDetails.Include(b => b.BookCopies).FirstOrDefault(b => b.ID == id);     

           //return context.BookDetails.FindAsync(id).Result;
        }

        public void UpdateBookDetails(BookDetails bookDetails)
        {
            context.Update(bookDetails);
            context.SaveChanges();
        }

        public BookCopy GetBookCopyById(int id)
        {
            //return context.Book.FindAsync(id).Result;
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
    }
}

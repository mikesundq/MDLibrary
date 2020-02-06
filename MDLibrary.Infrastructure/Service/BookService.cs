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
            context.Book.Add(book);
            context.SaveChanges();
        }

        public void AddNewBookDetails(BookDetails bookdetails)
        {
            context.Add(bookdetails);
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
            //var listOfBooksWithCorrectId = context.Book.Include(x => x.BookDetailsID).FindAll(b => b.BookDetailsID == id);
            return 1; //listOfBooksWithCorrectId.Count;
        }
    }
}

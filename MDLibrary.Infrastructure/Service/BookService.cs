using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;

namespace MDLibrary.Infrastructure.Service
{
    public class BookService : IBookServices
    {
        public List<BookDetails> ListOfBookDetails = new List<BookDetails>();
        public List<Book> ListOfBooks = new List<Book>();

        public void AddMoreCopiesOfBook(Book book)
        {
            bool excists = false;

            foreach(var bookdetail in ListOfBookDetails)
            {
                if(book.BookDetailsID == bookdetail.ID)
                {
                    excists = true;
                }
            }
            if(excists)
                ListOfBooks.Add(book);
        }

        public void AddNewBookDetails(BookDetails bookdetails)
        {
            ListOfBookDetails.Add(bookdetails);
        }


        public IList<BookDetails> ShowAllAvailableBooks()
        {
            throw new NotImplementedException();
        }

        public IList<BookDetails> ShowAllBooksByAuthor(int id)
        {
            return ListOfBookDetails.FindAll(b => b.AuthorID == id);
        }

        public int ShowNumberOfBooks(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Domain;

namespace MDLibrary.Application.Interfaces
{
    public interface IBookServices
    {
        public void AddNewBookDetails(BookDetails bookdetails);

        public BookDetails GetBookDetailsById(int id);

        public void AddMoreCopiesOfBook(Book book);
        public int ShowNumberOfBooks(int id);
        public IList<BookDetails> ShowAllAvailableBooks();

        public IList<BookDetails> ShowAllBooksByAuthor(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Domain;

namespace MDLibrary.Application.Interfaces
{
    public interface IBookServices
    {
        public void AddNewBookDetails(BookDetails bookdetails);

        public void AddMoreCopiesOfBook(BookCopy book);
        public int ShowNumberOfBooks(int id);

        public void DeleteBookDetailsByID(int id);
        public IList<BookDetails> ShowAllAvailableBooks();

        public IList<BookDetails> ShowAllBooksByAuthor(int id);

        public IList<BookDetails> ShowAllBookDetails();

        public BookDetails GetBookDetailsById(int id);

        public void UpdateBookDetails(BookDetails bookDetails);

        public BookCopy GetBookCopyById(int id);
        public IList<BookCopy> GetBookCopiesById(int[] ids);
    }
}

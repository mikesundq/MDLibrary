﻿using System;
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
        public IList<BookDetails> ShowAllAvailableBooks();

        public IList<BookDetails> ShowAllBooksByAuthor(int id);

        public IList<BookDetails> ShowAllBookDetails();
    }
}

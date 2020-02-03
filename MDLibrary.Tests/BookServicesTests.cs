﻿using MDLibrary.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MDLibrary.Domain;


namespace MDLibrary.Tests
{
    public class BookServicesTests
    {
        [Fact]
        public void AddNewBookDetails_AddBookDetailsForReference_ReturnCountOne()
        {
            //Arrange
            var testBookService = new BookService();
            var testBookDetails = new BookDetails() {
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 0,
                ISBN = 123456
            };
            
            var expectedResult = 1;
            //Act
            testBookService.AddNewBookDetails(testBookDetails);
            var actualResult = testBookService.ListOfBookDetails.Count;
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void AddMoreCopiesOfBook_AddOneBookToEmptyList_ReturnCountNrOne()
        {
            //Arrange
            var testBookService = new BookService();
            var testBookDetails = new BookDetails()
            {
                ID = 1,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 0,
                ISBN = 123456
            };
            var testBook = new Book()
            {
                BookDetailsID = 1
            };

            var expectedResult = 1;
            //Act
            testBookService.AddNewBookDetails(testBookDetails);
            testBookService.AddMoreCopiesOfBook(testBook);
            
            var actualResult = testBookService.ListOfBooks.Count;
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShowAllBooksByAuthor_AddAListOfBooksByAuthorsAndTryToGeTTheCorrectBack_ReturnsCountOfTwo()
        {
            //Arrange
            var testBookService = new BookService();
            var testBookDetails1 = new BookDetails()
            { 
                ID = 1,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 1,
                ISBN = 123456
            };
            testBookService.ListOfBookDetails.Add(testBookDetails1);
            var testBookDetails2 = new BookDetails()
            {
                ID = 2,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 1,
                ISBN = 123456
            };
            testBookService.ListOfBookDetails.Add(testBookDetails2);
            var testBookDetails3 = new BookDetails()
            {
                ID = 3,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 2,
                ISBN = 123456
            };
            testBookService.ListOfBookDetails.Add(testBookDetails3);
            var expectedResult = 2;
            //Act
            var booksByAuthor = testBookService.ShowAllBooksByAuthor(1);

            var actualResult = booksByAuthor.Count;
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShowNumberOfBooks_ListWithTwoBooksOfCorrectId_CountNumberTwo()
        {
            var testBookService = new BookService()
            {
                ListOfBooks =
                {
                    new Book() {BookDetailsID = 2},
                    new Book() {BookDetailsID = 2},
                    new Book() {BookDetailsID = 3},
                }
            };

            var expectedCountNr = 2;

            var actualCountNr = testBookService.ShowNumberOfBooks(2);

            Assert.Equal(expectedCountNr, actualCountNr);
            
        }

        [Fact]
        public void ShowAllBookDetails_ListOfAllBooksDetals_CountNrThree()
        {
            //Arrange
            var testBookService = new BookService();
            var testBookDetails1 = new BookDetails()
            {
                ID = 1,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 1,
                ISBN = 123456
            };
            testBookService.ListOfBookDetails.Add(testBookDetails1);
            var testBookDetails2 = new BookDetails()
            {
                ID = 2,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 1,
                ISBN = 123456
            };
            testBookService.ListOfBookDetails.Add(testBookDetails2);
            var testBookDetails3 = new BookDetails()
            {
                ID = 3,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 2,
                ISBN = 123456
            };
            testBookService.ListOfBookDetails.Add(testBookDetails3);
            var expectedCountNr = 3;
            //Act
            var actualCountNr = testBookService.ShowAllBookDetails().Count;

            //Assert
            Assert.Equal(expectedCountNr, actualCountNr);
        }
    }
}

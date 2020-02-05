using MDLibrary.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MDLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using MDLibrary.Infrastructure.Persistence;
using System.Linq;

namespace MDLibrary.Tests
{
    public class BookServicesTests
    {
        [Fact]
        public void AddNewBookDetails_AddBookDetailsForReference_ReturnCountOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MDLibrary_AddNewBookDetails")
                .Options;

            var context = new ApplicationDbContext(options);

            var testBookService = new BookServices(context);

            var expectedResult = 1;

            var testBook = new BookDetails()
            {
                ID = 1,
                Titel = "En testboks historia",
                ISBN = "1234567891012",
                AuthorID = 1
            };

            //Act
            testBookService.AddNewBookDetails(testBook);
            var actualResult = context.BookDetails.ToList().Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void AddMoreCopiesOfBook_AddOneBookToEmptyList_ReturnCountNrOne()
        {
            ////Arrange

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_AddMoreCopies")
                .Options;

            var context = new ApplicationDbContext(options);

            var expectedResult = 1;

            var testBook = new BookCopy()
            {
                ID = 1,
                BookDetailsID = 2
            };

            var testBookService = new BookServices(context);
            //Act

            testBookService.AddMoreCopiesOfBook(testBook);

            var actualResult = context.Book.ToList().Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShowAllBooksByAuthor_AddAListOfBooksByAuthorsAndTryToGeTTheCorrectBack_ReturnsCountOfTwo()
        {
            //Arrange
            var testBookService = new BookServices(null);
            /*  var testBookDetails1 = new BookDetails()
              { 
                  ID = 1,
                  Titel = "C# for dummies",
                  Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                  AuthorID = 1,
                  ISBN = "123456"
              };
              testBookService.ListOfBookDetails.Add(testBookDetails1);
              var testBookDetails2 = new BookDetails()
              {
                  ID = 2,
                  Titel = "C# for dummies",
                  Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                  AuthorID = 1,
                  ISBN = "123456"
              };
              testBookService.ListOfBookDetails.Add(testBookDetails2);
              var testBookDetails3 = new BookDetails()
              {
                  ID = 3,
                  Titel = "C# for dummies",
                  Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                  AuthorID = 2,
                  ISBN = "123456"
              };
              testBookService.ListOfBookDetails.Add(testBookDetails3); */
              var expectedResult = 2;
              //Act
            //  var booksByAuthor = testBookService.ShowAllBooksByAuthor(1);
              
            var actualResult = 1; //booksByAuthor.Count;
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShowNumberOfBooks_ListWithTwoBooksOfCorrectId_CountNumberTwo()
        {
          /*  var testBookService = new BookService(null)
            {
                ListOfBooks =
                {
                    new BookCopy() {BookDetailsID = 2},
                    new BookCopy() {BookDetailsID = 2},
                    new BookCopy() {BookDetailsID = 3},
                }
            }; */

            var expectedCountNr = 2;

            var actualCountNr = 1; //testBookService.ShowNumberOfBooks(2);

            Assert.Equal(expectedCountNr, actualCountNr);
            
        }

        [Fact]
        public void ShowAllBookDetails_ListOfAllBooksDetals_CountNrThree()
        {
            //Arrange
            var testBookService = new BookServices(null);
            /*var testBookDetails1 = new BookDetails()
            {
                ID = 1,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 1,
                ISBN = "123456"
            };
            testBookService.ListOfBookDetails.Add(testBookDetails1);
            var testBookDetails2 = new BookDetails()
            {
                ID = 2,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 1,
                ISBN = "123456"
            };
            testBookService.ListOfBookDetails.Add(testBookDetails2);
            var testBookDetails3 = new BookDetails()
            {
                ID = 3,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 2,
                ISBN = "123456"
            };
            testBookService.ListOfBookDetails.Add(testBookDetails3); */
            var expectedCountNr = 3;
            //Act
            var actualCountNr = 1; // testBookService.ShowAllBookDetails().Count;

            //Assert
            Assert.Equal(expectedCountNr, actualCountNr);
        }

        private void Seed(ApplicationDbContext context)
        {
            var authors = new[]
            {
                new Author { ID = 1, Name = "Test Author 1"},
                new Author { ID = 2, Name = "Test Author 2"}
            };
            var bookDetails = new[]
            {
                new BookDetails { ID = 1, AuthorID = 1, ISBN = "1234567891012", Titel = "Bok Titel 1"},
                new BookDetails { ID = 2, AuthorID = 1, ISBN = "1234567891013", Titel = "Bok Titel 2"},
                new BookDetails { ID = 3, AuthorID = 2, ISBN = "1234567891014", Titel = "Bok Titel 3"}
            };
            var books = new[]
            {
                new BookCopy { ID = 1, BookDetailsID = 1},
                new BookCopy { ID = 2, BookDetailsID = 1},
                new BookCopy { ID = 3, BookDetailsID = 2},
                new BookCopy { ID = 4, BookDetailsID = 3}
            };
            context.AddRange(authors);
            context.AddRange(bookDetails);
            context.AddRange(books);
            context.SaveChanges();
        }
    }
}

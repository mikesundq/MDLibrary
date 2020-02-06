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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_ShowAllBooksByAúthor")
                .Options;

            var context = new ApplicationDbContext(options);

            Seed(context);

            var testBookService = new BookServices(context);

            var expectedResult = 2;

            //Act
            var actualResult = testBookService.ShowAllBooksByAuthor(1).Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShowNumberOfBooks_ListWithTwoBooksOfCorrectId_CountNumberTwo()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_ShowNumberOfBooks")
                .Options;
            var context = new ApplicationDbContext(options);
            var expectedCountNr = 2;
            Seed(context);
            var testBookSerice = new BookServices(context);

            //Act
            var actualCountNr = testBookSerice.ShowNumberOfBooks(1);

            //Assert
            Assert.Equal(expectedCountNr, actualCountNr);
            
        }

        [Fact]
        public void ShowAllBookDetails_ListOfAllBooksDetals_CountNrThree()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_ShowAllBookDetails")
                .Options;

            var context = new ApplicationDbContext(options);

            Seed(context);

            var testBookService = new BookServices(context);
            var bookDetails = new[]
{
                new BookDetails { ID = 1, AuthorID = 1, ISBN = "1234567891012", Titel = "Bok Titel 1"},
                new BookDetails { ID = 2, AuthorID = 1, ISBN = "1234567891013", Titel = "Bok Titel 2"},
                new BookDetails { ID = 3, AuthorID = 2, ISBN = "1234567891014", Titel = "Bok Titel 3"}
            };

            List<string> expectedList = new List<string>();

            foreach(var book in bookDetails)
            {
                expectedList.Add(book.Titel);
            }

            //Act
            List<BookDetails> actualList = new List<BookDetails>(); 
            actualList = testBookService.ShowAllBookDetails().ToList();


            //Assert
            int i = 0;
            foreach(var book in actualList)
            {
                Assert.Equal(expectedList[i], book.Titel);
                i++;
            }
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

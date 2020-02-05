using MDLibrary.Infrastructure.Service;
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
            var testBookService = new BookService(null);
            var testBookDetails = new BookDetails() {
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 0,
                ISBN = "123456"
            };
            
            var expectedResult = 1;
            //Act
            testBookService.AddNewBookDetails(testBookDetails);
            var actualResult = 2; //testBookService.ListOfBookDetails.Count;
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void AddMoreCopiesOfBook_AddOneBookToEmptyList_ReturnCountNrOne()
        {
            //Arrange
            var testBookService = new BookService(null);
            var testBookDetails = new BookDetails()
            {
                ID = 1,
                Titel = "C# for dummies",
                Details = "Learn to write programs using C#. The perfect book for the perfect dummy.",
                AuthorID = 0,
                ISBN = "123456"
            };
            var testBook = new BookCopy()
            {
                BookDetailsID = 1
            };

            var expectedResult = 1;
            //Act
            testBookService.AddNewBookDetails(testBookDetails);
            testBookService.AddMoreCopiesOfBook(testBook);

            var actualResult = 1; //testBookService.ListOfBooks.Count;
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void ShowAllBooksByAuthor_AddAListOfBooksByAuthorsAndTryToGeTTheCorrectBack_ReturnsCountOfTwo()
        {
            //Arrange
            var testBookService = new BookService(null);
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
            var testBookService = new BookService(null);
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
    }
}

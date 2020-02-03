using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Service; 

namespace MDLibrary.Tests
{
    public class AuthorServiceTests
    {

        [Fact]
        public void AddAuthor_AddAuthorToEmptyList_CountNrOne()
        {

            //ARRANGE
            var testAuthorService = new AuthorService();
            var testAuthor = new Author()
            {
                Name = "Mike"
            };
            var expectedCountNr = 1;
            //ACT
            testAuthorService.AddAuthor(testAuthor);

            var acctualCountNr = testAuthorService.Authors.Count;

            //ASSERT

            Assert.Equal(expectedCountNr, acctualCountNr);
        }

        [Fact]
        public void RemoveAuthor_RemoveAuthorFromList_CountNrZero()
        {
            //Arrange
            var testAuthorService = new AuthorService();
            var testAuthor = new Author()
            {
                ID = 1,
                Name = "Daniel-san"
            };
            testAuthorService.Authors.Add(testAuthor);
            var expectedCountNr = 0;

            //Act
            testAuthorService.RemoveAuthor(1);
            var actualCountNr = testAuthorService.Authors.Count;

            //Assert
            Assert.Equal(expectedCountNr, actualCountNr);
        }


        [Fact]
        public void GetAllAuthors_ListOfTwoAuthors_CountNrTwo()
        {
            //ARRANGE
            var testAuthorService = new AuthorService()
            {
                Authors = {
                    new Author() { Name = "Mike" }, 
                    new Author() { Name = "Danne" }
                }
            };

            var expectedCountNr = 2;

            //ACT

            var actualCountNr = testAuthorService.Authors.Count;
            //ASSERT
            Assert.Equal(expectedCountNr, actualCountNr);
        }
    }
}

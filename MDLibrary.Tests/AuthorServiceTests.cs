using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using MDLibrary.Infrastructure.Persistence;
using System.Linq;

namespace MDLibrary.Tests
{
    public class AuthorServiceTests
    {

        [Fact]
        public void AddAuthor_AddAuthorToEmptyList_CountNrOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MDLibrary_AddAuthor")
                .Options;

            var context = new ApplicationDbContext(options);

            var testAuthorService = new AuthorService(context);

            var testAuthor = new Author()
            {
                ID = 1,
                Name = "Test Testsson"
            };
            var expectedResult = 1;
            //Act
            testAuthorService.AddAuthor(testAuthor);

            //Assert
            var actualResult = context.Author.ToList().Count;

            //Arrange
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void RemoveAuthor_RemoveAuthorFromList_CountNrOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MDLibrary_RemoveAuthor")
                .Options;

            var context = new ApplicationDbContext(options);

            Seed(context);

            var testAuthorService = new AuthorService(context);

            var expectedCount = 1;

            //Act

            testAuthorService.RemoveAuthor(2);

            var acctualCount = context.Author.ToList().Count;

            //Assert
            Assert.Equal(expectedCount, acctualCount);
        }


        [Fact]
        public void GetAllAuthors_ListOfTwoAuthors_CountNrTwo()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MDLibrary_AuthorGetAllAuthors")
                .Options;

            var context = new ApplicationDbContext(options);

            Seed(context);

            var testAuthorService = new AuthorService(context);

            var expectedResult = 2;

            //ACT
            var actualResult = testAuthorService.GetAllAuthors().Count;

            //ASSERT
            Assert.Equal(expectedResult, actualResult);
        }

        private void Seed(ApplicationDbContext context)
        {
            var authors = new[]
            {
                new Author { ID = 1, Name = "Test Testsson" },
                new Author { ID = 2, Name = "Sven Svensson" }
            };
            context.Author.AddRange(authors);
            context.SaveChanges();
        }
    }
}

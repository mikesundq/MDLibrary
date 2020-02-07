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
    public class LoanServiceTest
    {
        [Fact]
        public void LoanOutBook_AddOneLoanToEmptyList_CountNrOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_LoanOutBook")
                .Options;

            var context = new ApplicationDbContext(options);

            var testLoanService = new LoanService(context);
            var expectedResult = 1;

            //Act
            testLoanService.LoanOutBook(new Loan() { ID = 1, BookCopyID = 1, MemberID = 1 });
            var actualResult = context.Loan.Find(1).BookCopyID;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void LoanService_ReturnOneBook_CountNrZeroOnLoanList()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_ReturnOneBook")
                .Options;

            var context = new ApplicationDbContext(options);

            var testLoanService = new LoanService(context);

            context.Loan.Add(new Loan { ID = 1, BookCopyID = 1, MemberID = 1 });
            var expectedResult = 0;

            //Act
            testLoanService.ReturnBook(1);
            var actualResult = context.Loan.ToList().Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShowAllBooksLoanedByMember_ListWithTwoLoansFromMember_CorrectBookIDs()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_ShowAllBooksLoanedByMember")
                .Options;

            var context = new ApplicationDbContext(options);

            BookCopy[] bookCopies =
            {
                new BookCopy() { ID = 1, BookDetailsID = 1 },
                new BookCopy() { ID = 2, BookDetailsID = 2 },
                new BookCopy() { ID = 3, BookDetailsID = 3 }
            };
            Loan[] loans =
            {
                new Loan() {ID = 1, MemberID = 1, BookCopyID = bookCopies[0].ID },
                new Loan() {ID = 2, MemberID = 2, BookCopyID = bookCopies[1].ID },
                new Loan() {ID = 3, MemberID = 2, BookCopyID = bookCopies[2].ID }
            };

            context.Loan.AddRange(loans);
            context.SaveChanges();

            var testLoanService = new LoanService(context);

            var expectedResultOne = 2;
            var expectedResultTwo = 3;

            //Act
            var actualResult = testLoanService.ShowAllBooksLoanedByMember(2);

            //Assert
            Assert.Equal(expectedResultOne, actualResult[0].BookCopyID);
            Assert.Equal(expectedResultTwo, actualResult[1].BookCopyID);

        }

        [Fact]
        public void GetAllLoans_GetAllLoansFromList_CountTwoLoans()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_GetAllLoans")
                .Options;

            var context = new ApplicationDbContext(options);

            Loan[] loans =
            {
                new Loan() { ID = 1, BookCopyID = 2, MemberID = 2},
                new Loan() { ID = 2, BookCopyID = 5, MemberID = 5}
            };

            context.Loan.AddRange(loans);
            context.SaveChanges();

            var testLoanService = new LoanService(context);
            var expectedResult = 2;

            //Act
            var actualResult = testLoanService.GetAllLoans().Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}

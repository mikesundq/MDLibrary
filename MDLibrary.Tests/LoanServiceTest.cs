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
            var newLoan = new Loan() { ID = 1, MemberID = 1 };
            testLoanService.LoanOutBook(newLoan);
            var newBookCopy = new BookCopy() { ID = 1, LoanID = newLoan.ID };
            context.BookCopy.Add(newBookCopy);
            context.SaveChanges();
            var actualResult = context.BookCopy
                .Where(l =>l.LoanID == 1).ToList().Count;

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
            context.Loan.Add(new Loan { ID = 1, MemberID = 1 });
            context.SaveChanges();
            
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

            Loan[] loans =
{
                new Loan() {ID = 1, MemberID = 1},
                new Loan() {ID = 2, MemberID = 2},
                new Loan() {ID = 3, MemberID = 2}
            };

            context.Loan.AddRange(loans);
            context.SaveChanges();

            var testLoanService = new LoanService(context);

            var expectedResultOne = 2;
            //var expectedResultTwo = 2;

            //Act
            var actualResult = testLoanService.ShowAllBooksLoanedByMember(2).Count;

            //Assert
            Assert.Equal(expectedResultOne, actualResult);
            //Assert.Equal(expectedResultTwo, actualResult[1].BookCopyID);
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
                new Loan() {ID = 2, MemberID = 2},
                new Loan() {ID = 3, MemberID = 2}
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

        [Fact]
        public void GetMemberAndBooksFromLoan_GetCombinedData_MatchMemberIDAndCountOfTwoBooks()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("MDLibrary_GetMemberAndBooksFromLoan")
    .Options;

            var context = new ApplicationDbContext(options);

            Loan[] loans =
{
                new Loan() {ID = 1, MemberID = 1},
                new Loan() {ID = 2, MemberID = 2},
                new Loan() {ID = 3, MemberID = 2}
            };

            BookCopy[] bookCopies =
            {
                new BookCopy() { ID = 1, BookDetailsID = 1, LoanID = 1 },
                new BookCopy() { ID = 2, BookDetailsID = 2, LoanID = 1 },
                new BookCopy() { ID = 3, BookDetailsID = 3, LoanID = 2 }
            };

            context.Loan.AddRange(loans);
            context.BookCopy.AddRange(bookCopies);
            context.SaveChanges();

            var testLoanService = new LoanService(context);

            var expectedMemberID = 1;
            var expectedLoanCount = 2;

            //Act
            var loanForTest = context.Loan
                .Where(l => l.MemberID == 1).First();
            var actualMemberID = loanForTest.MemberID;

            var actualBookCopiesCount = context.BookCopy
                .Where(bc => bc.LoanID == loanForTest.ID).ToList().Count;

            //Assert
            Assert.Equal(expectedMemberID, actualMemberID);
            Assert.Equal(expectedLoanCount, actualBookCopiesCount);
        }
    }
}

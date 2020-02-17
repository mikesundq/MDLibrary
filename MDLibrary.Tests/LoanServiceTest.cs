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
            var newLoanBook = new LoanBook() { LoanID = 1, BookCopyID = 2 };
            context.LoanBook.Add(newLoanBook);
            context.SaveChanges();
            var actualResult = context.LoanBook
                .Where(l => l.LoanID == 1).ToList().Count;

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

            context.Member.Add(new Member { ID = 1 });
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
            };

            LoanBook[] loanBooks = new[] {
                new LoanBook { LoanID = 1},
                new LoanBook { LoanID = 2},
                new LoanBook { LoanID = 2}
            };


            context.LoanBook.AddRange(loanBooks);
            context.Loan.AddRange(loans);
            context.SaveChanges();

            var testLoanService = new LoanService(context);

            var expectedResult = 2;
            //var expectedResultTwo = 2;

            //Act
            var bookLoansByMember = testLoanService.ShowAllBooksLoanedByMember(2);
            var actualResult = bookLoansByMember[0].LoanBooks.Count();

            //Assert
            Assert.Equal(expectedResult, actualResult);
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

            //LoanBook[] loanBooks =
            //{
            //    new LoanBook {LoanID = 2},
            //    new LoanBook {LoanID = 3},

            //};
            //BookCopy[] bookCopies =
            //{
            //    new BookCopy{BookDetailsID = 1},
            //    new BookCopy{BookDetailsID = 2}
            //};
            //BookDetails[] bookDetails =
            //{
            //    new BookDetails {ID = 1},
            //    new BookDetails {ID = 2}
            //};
            Member[] members =
            {
                new Member{ID=2}
            };
            context.Member.AddRange(members);
            //context.BookDetails.AddRange(bookDetails);
            //context.BookCopy.AddRange(bookCopies);
            //context.LoanBook.AddRange(loanBooks);
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
                new BookCopy() { ID = 1, BookDetailsID = 1},
                new BookCopy() { ID = 2, BookDetailsID = 2},
                new BookCopy() { ID = 3, BookDetailsID = 3}
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

            //var actualBookCopiesCount = context.BookCopy
            //    .Where(bc => bc.LoanID == loanForTest.ID).ToList().Count;

            //Assert
            Assert.Equal(expectedMemberID, actualMemberID);
            //Assert.Equal(expectedLoanCount, actualBookCopiesCount);
        }
    }
}

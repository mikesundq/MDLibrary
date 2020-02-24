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

            var newLoan = new Loan() { ID = 1, MemberID = 1 };
            newLoan.BookCopies = new List<BookCopy> { new BookCopy() { ID = 2 } }; 

            //Act
            testLoanService.LoanOutBook(newLoan);
            var actualResult = context.Loan
                .Where(l => l.ID == 1).ToList().Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void LoanService_ReturnOneBook_CountNrOneOnLoanList()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_ReturnOneBook")
                .Options;

            var context = new ApplicationDbContext(options);

            var testLoanService = new LoanService(context);

            context.Member.Add(new Member { ID = 1 });
            context.Loan.Add(new Loan { ID = 1, MemberID = 1 });
            context.LoanBook.AddRange(new LoanBook { BookCopyID = 1, LoanID = 1 }, new LoanBook { BookCopyID = 2, LoanID = 1 });

            context.SaveChanges();
            
            var expectedResult = 1;

            //Act
            testLoanService.ReturnOneBook(1);
            var actualResult = context.LoanBook.ToList().Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShowAllLoansByMember_ListWithTwoLoansFromMember_CorrectBookIDs()
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
            var bookLoansByMember = testLoanService.ShowAllLoansByMember(2);
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
          
            Member[] members =
            {
                new Member{ID=2}
            };
            context.Member.AddRange(members);
            
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

        [Fact]
        public void GetAllBooksOnLoan_ReturnAListOfBookCopies_ReturnThree()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MDLibrary_GetBookCopiesFromLoan")
                .Options;

            var context = new ApplicationDbContext(options);

            LoanBook[] loanBooks =
            {
                new LoanBook() {BookCopyID = 1, LoanID = 1},
                new LoanBook() {BookCopyID = 2, LoanID = 1},
                new LoanBook() {BookCopyID = 3, LoanID = 1}
            };

            BookCopy[] bookCopies =
            {
                new BookCopy() { ID = 1, BookDetailsID = 1},
                new BookCopy() { ID = 2, BookDetailsID = 1},
                new BookCopy() { ID = 3, BookDetailsID = 1}
            };

            context.Loan.Add(new Loan { ID = 1, MemberID = 1 });
            context.Member.Add(new Member { ID = 1 });
            context.LoanBook.AddRange(loanBooks);
            context.BookCopy.AddRange(bookCopies);
            context.BookDetails.Add(new BookDetails { ID = 1, AuthorID = 1 });
            context.Author.Add(new Author { ID = 1 });
            context.SaveChanges();

            var testLoanService = new LoanService(context);

            var expectedCount = 3;

            //Act
            var actualCount = testLoanService.GetAllBooksOnLoan().Count;

            //Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void ReturnAllBooks_ReturnAllBooksInLoanById_CountOne()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ReturnAllBooksInLoan")
                .Options;

            var context = new ApplicationDbContext(options);
            context.Loan.Add(new Loan { ID = 1, MemberID = 1});
            context.Member.Add(new Member { ID = 1 });

            context.LoanBook.AddRange(
                new LoanBook { BookCopyID = 3, LoanID = 1}, 
                new LoanBook { BookCopyID = 2, LoanID = 1}, 
                new LoanBook { BookCopyID = 1, LoanID = 1},
                new LoanBook { BookCopyID = 4, LoanID = 2}
                );
            
            context.SaveChanges();

            var expectedCount = 1;

            var testLoanService = new LoanService(context);

            //Act
            testLoanService.ReturnAllBooks(1);

            var actualCount = context.Loan.Where(l => l.IsReturned == 1).ToList().Count;

            //Assert
            Assert.Equal(expectedCount, actualCount);

        }

        [Fact]
        public void ShowAllBooksNotOnLoan_ReturnListOfBookCopies_ReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ShowAllBooksNotOnLoan").Options;

            var context = new ApplicationDbContext(options);
            //books in library
            context.BookCopy.AddRange(
                new BookCopy { ID = 1, BookDetailsID = 1 },
                new BookCopy { ID = 2, BookDetailsID = 1 },
                new BookCopy { ID = 3, BookDetailsID = 1 },
                new BookCopy { ID = 4, BookDetailsID = 1 },
                new BookCopy { ID = 5, BookDetailsID = 1 }
                );
            //books on loan
            context.LoanBook.AddRange(
                new LoanBook { BookCopyID = 1},
                new LoanBook { BookCopyID = 2}
                );
            //bokkdetails
            context.BookDetails.Add(new BookDetails { ID = 1, AuthorID = 1 });
            context.Author.Add(new Author { ID = 1 });
            context.SaveChanges();

            var testLoanService = new LoanService(context);

            var expectedCount = 3;

            var actualCount = testLoanService.ShowAllBooksNotOnLoan().Count;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void CalculateLateFee_LoanWithLateBooks_CorrectLateFee()
        {
            //Arrange
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CalculateLateFeeInLoans").Options;
            var context = new ApplicationDbContext(options);

            var testLoanSerice = new LoanService(context);
            
            var lateDate = DateTime.Today.AddDays(-3);

            var expectedLateFee = 36;
            //Act

            var actualLateFee = testLoanSerice.CalculateLateFee(lateDate);

            //Assert

            Assert.Equal(expectedLateFee, actualLateFee);

        }
    }
}

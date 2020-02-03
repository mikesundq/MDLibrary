using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Service;

namespace MDLibrary.Tests
{
    public class LoanServiceTest
    {
        [Fact]
        public void LoanOutBook_AddOneLoanToEmptyList_CountNrOne()
        {
            var testLoanService = new LoanService();
            var expectedCountNr = 1;
            var testLoan = new Loan();

            testLoanService.LoanOutBook(testLoan);

            var actualCountNr = testLoanService.Loans.Count;

            Assert.Equal(expectedCountNr, actualCountNr);
        }

        [Fact]
        public void LoanService_ReturnOneBook_CountNrZeroOnLoanList()
        {
            //Arrange
            var testLoanService = new LoanService();
            var testLoan = new Loan
            {
                ID = 1
            };
            testLoanService.Loans.Add(testLoan);

            var expectedResult = 0;

            //Act
            testLoanService.ReturnBook(testLoan.ID);
            var actualResult = testLoanService.Loans.Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Theory]
        [InlineData(1,12)]
        [InlineData(2,20)]
        public void ShowAllBooksLoanedByMember_ListWithTwoLoansFromMember_CorrectBookID(int testNr, int expectedBookID)
        {
            //Arrange
            //var testBookService = new BookService()
            //{
            //    ListOfBooks =
            //    {
            //        new Book() {ID = 23},
            //        new Book() {ID = 12},
            //        new Book() {ID = 20},
            //    }
            //};
            var testLoanService = new LoanService()
            {
                Loans =
                {
                    new Loan() {MemberID = 1, ID = 1, BookID = 23},
                    new Loan() {MemberID = 2, ID = 2, BookID = 12},
                    new Loan() {MemberID = 2, ID = 3, BookID = 20},
                }
            };

            int actualBookID;
            var listOfBookSFromMember = testLoanService.ShowAllBooksLoanedByMember(2);

            //Act
            switch (testNr)
            {
                case 1:
                    actualBookID = listOfBookSFromMember[0];
                    break;
                case 2:
                    actualBookID = listOfBookSFromMember[1];
                    break;
                default:
                    actualBookID = 0;
                    break;
            }

            //Assert
            Assert.Equal(expectedBookID, actualBookID);
        }
    }
}

using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.LoanVM
{
    public class LoanIndexVm
    {
        public IList<Loan> Loans { get; set; } = new List<Loan>();
        public IList<BookCopy> BookCopy { get; set; } = new List<BookCopy>();

        public int NrOfBooksInLoan { get; set; }
    }
}

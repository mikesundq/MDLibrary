using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Domain
{
    public class LoanBook
    {
        public int BookCopyID { get; set; }
        public BookCopy BookCopy { get; set; }
        public int LoanID { get; set; }
        public Loan Loan { get; set; }
    }
}

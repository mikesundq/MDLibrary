using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Domain
{
    public class Loan
    {
        public int ID { get; set; }
        public DateTime TimeOfLoan { get; set; }
        public DateTime TimeToReturnBook { get; set; }
        public IList<BookCopy> BookCopies { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }
    }
}

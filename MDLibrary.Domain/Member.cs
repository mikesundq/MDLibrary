using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Domain
{
    class Member
    {
        public int ID { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public IList<Loan> Loans { get; set; }
    }
}

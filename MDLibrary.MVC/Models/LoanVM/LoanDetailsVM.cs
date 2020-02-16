using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.LoanVM
{
    public class LoanDetailsVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<Loan> Loans { get; set; } = new List<Loan>();
        public IList<BookDetails> BookDetails { get; set; } = new List<BookDetails>();
    }
}

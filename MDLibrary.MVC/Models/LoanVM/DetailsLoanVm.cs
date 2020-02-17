using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.LoanVM
{
    public class DetailsLoanVm
    {
        public int ID { get; set; }
        [Display(Name = "Time of loan")]
        public DateTime TimeOfLoan { get; set; }
        [Display(Name = "Time to return loan")]
        public DateTime TimeToReturnBook { get; set; }
        public IList<LoanBook> LoanBooks { get; set; } = new List<LoanBook>();
        public int MemberID { get; set; }
        public Member Member { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.LoanVM
{
    public class CreateLoanVm
    {
        [Required]
        [DisplayName("Loan date")]
        public DateTime TimeOfLoan { get; set; }
        [DisplayName("Date to return book")]
        public string TimeToReturnBook { get; set; }
        [Required]
        [DisplayName("Title")]
        public SelectList BookCopyName { get; set; }
        [Required]
        [DisplayName("Member")]
        public SelectList MemberName { get; set; }
    }
}

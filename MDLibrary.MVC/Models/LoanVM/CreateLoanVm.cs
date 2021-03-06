﻿using MDLibrary.Domain;
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
        public DateTime TimeOfLoan { get; set; } = DateTime.Today;
        [Display(Name = "Time of loan")]
        public string DisplayDate { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
        [DisplayName("Date to return loan")]
        public string TimeToReturnBook { get; set; } = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
        [DisplayName("Books")]
        public IList<BookDetails> Books { get; set; }
        public IList<BookCopy> AvalibleBooks { get; set; }
        public SelectList Members { get; set; }
        [Required]
        public int MemberID { get; set; }
    }
}

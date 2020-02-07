using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MDLibrary.MVC.Models.MemberVM
{
    public class CreateMemberVm
    {
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [Display(Name = "Social Security Number", Order = 2)]
        [RegularExpression(@"^\d{10}$",ErrorMessage = "This is not a valid SSN")] //[RegularExpression(@"^\d+$")] (@"^\d{5,10}$") d=digit
        public string SSN { get; set; }
        [Display(Order =1)]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

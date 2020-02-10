using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.MemberVM
{
    public class EditMemberVm
    {
        //[ReadOnly(true)]
        //public int Id { get; private set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [Display(Name = "Social Security Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "This is not a valid SSN")]
        public string SSN { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

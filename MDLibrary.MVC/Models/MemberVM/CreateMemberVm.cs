using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MDLibrary.MVC.Models.MemberVM
{
    public class CreateMemberVm
    {
        public int ID { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public SelectList Loans { get; set; }
    }
}

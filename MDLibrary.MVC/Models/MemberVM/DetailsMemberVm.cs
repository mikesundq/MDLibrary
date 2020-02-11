using MDLibrary.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.MemberVM
{
    public class DetailsMemberVm
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<Loan> Loans { get; set; } = new List<Loan>();
    }
}

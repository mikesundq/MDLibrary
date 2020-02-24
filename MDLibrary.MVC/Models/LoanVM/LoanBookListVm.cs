using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.LoanVM
{
    public class LoanBookListVm
    {
        public IList<LoanBook> BookCopies { get; set; } = new List<LoanBook>();
        
    }
}

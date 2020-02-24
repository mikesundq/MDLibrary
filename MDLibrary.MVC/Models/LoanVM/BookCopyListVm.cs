using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.LoanVM
{
    public class BookCopyListVm
    {
        public IList<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
        
    }
}

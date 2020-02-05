using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.BooksVM
{
    public class BookIndexVm
    {
        public IList<BookDetails> Books { get; set; } = new List<BookDetails>();
    }
}

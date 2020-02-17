using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Domain
{
    public class BookCopy
    {
        public int ID { get; set; }
        public int BookDetailsID { get; set; }
        public BookDetails BookDetails { get; set; }
        
    }
}

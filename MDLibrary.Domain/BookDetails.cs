using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Domain
{
    public class BookDetails
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string Titel { get; set; }
        public int AuthorID { get; set; } //Foreign key
        public Author Author { get; set; }
        public string Details { get; set; }
        public IEnumerable<BookCopy> BookCopies{ get; set; }
    }
}

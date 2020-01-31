using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Domain
{
    public class BookDetails
    {
        public int ID { get; set; }
        public int ISBN { get; set; }
        public string Titel { get; set; }
        
        public int AuthorID { get; set; } //Foreign key
        public Author Author { get; set; }
        public string Details { get; set; }
        public IEnumerable<Book> Books{ get; set; }
    }
}

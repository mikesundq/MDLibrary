using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Domain
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }

        //Join query to fill this list using code. Not on DB.
        //public IEnumerable<Book> Books { get; set; }
    }
}

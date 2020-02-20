using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.AuthorVM
{
    public class AuthorIndexVm
    {
        public IList<Author> Authors { get; set; } = new List<Author>();
    }
}

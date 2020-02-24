using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.AuthorVM
{
    public class DetailsAuthorVm
    {

        public int ID { get; set; }
        [Display(Name = "Name of Author")]
        public string Name { get; set; }
        [Display(Name = "Books written by author")]
        public IList<BookDetails> WrittenBooks { get; set; }

        public bool CanBeRemoved { get; set; }
    }
}

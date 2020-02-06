using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.BooksVM
{
    public class BookDetailsVm
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(13)]
        [MinLength(13)]
        public string ISBN { get; set; }
        [Required]
        public string Titel { get; set; }
        public string Author { get; set; }
        [Required]
        public int AuthorID { get; set; }
        public string Details { get; set; }
    }
}

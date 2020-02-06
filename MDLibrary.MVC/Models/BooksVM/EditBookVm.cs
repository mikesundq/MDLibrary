using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.BooksVM
{
    public class EditBookVm
    {
        [Required]
        [MaxLength(13)]
        [MinLength(13)]
        public string ISBN { get; set; }
        [Required]
        public string Titel { get; set; }
        public SelectList Authors { get; set; }
        [Required]
        public int AuthorID { get; set; }
        public string Details { get; set; }
    }
}

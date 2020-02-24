using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;
using MDLibrary.Application.Interfaces;
using MDLibrary.MVC.Models.AuthorVM;

namespace MDLibrary.MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorsService;
        private readonly IBookServices bookService;

        public AuthorsController(IAuthorService authorService, IBookServices bookService)
        {
            this.authorsService = authorService;
            this.bookService = bookService;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var vm = new AuthorIndexVm();

            vm.Authors = authorsService.GetAllAuthors();

            return View(vm);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var author = authorsService.GetAuthorById(id);
            var books = bookService.ShowAllBooksByAuthor(id);

            var vm = new DetailsAuthorVm();
            vm.Name = author.Name;
            vm.WrittenBooks = books;
            return View(vm);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorVm vm)
        {
            if (ModelState.IsValid)
            {
                //var vm = new CreateAuthorVm();
                var authorToAdd = new Author();
                authorToAdd.Name = vm.Name;
                authorsService.AddAuthor(authorToAdd);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        
        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var author = authorsService.GetAuthorById(id);
            
            if (author == null)
                return NotFound();
            
            var vm = new EditAuthorVm();
            vm.Name = author.Name;
            
            return View(vm);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] EditAuthorVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var author = authorsService.GetAuthorById(id);
            author.Name = vm.Name;
            authorsService.EditAuthor(author);

            return RedirectToAction(nameof(Index));
            
        }
        
        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var author = authorsService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            authorsService.RemoveAuthor(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}

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
        /*
        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Author author)
        {
            if (id != author.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.ID == id);
        }
        */
    }
}

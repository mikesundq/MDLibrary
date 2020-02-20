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
using MDLibrary.MVC.Models.BooksVM;

namespace MDLibrary.MVC.Controllers
{
    public class BookDetailsController : Controller
    {
        
        private readonly IBookServices bookService;
        private readonly IAuthorService authorService;
        private readonly ILoanService loanService;

        public BookDetailsController(IBookServices bookService, IAuthorService authorService, ILoanService loanService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.loanService = loanService;
        }

        // GET: BookDetails
        public async Task<IActionResult> Index()
        {
            var vm = new BookIndexVm(); //Create a viewmodel object
            vm.Books = bookService.ShowAllBookDetails(); //Get all available books to show
            return View(vm); //Send the VM object to the view
        }

        // GET: BookDetails
        public async Task<IActionResult> IndexAvalibleBooks()
        {
            var vm = new BookCopyIndexVm(); //Create a viewmodel object
            vm.BookCopies = loanService.ShowAllBooksNotOnLoan(); //Get all available books to show
            return View(vm); //Send the VM object to the view
        }

        public async Task<IActionResult> IndexBooksOnLoan()
        {
            var vm = new BookCopyIndexVm(); //Create a viewmodel object
            vm.BookCopies = loanService.ShowAllBooksOnLoan(); //Get all available books to show
            return View(vm); //Send the VM object to the view
        }

        
        public async Task<IActionResult> AddOneMoreBookCopy(int id)
        {
            var newBook = new BookCopy();
            newBook.BookDetailsID = id;

            bookService.AddMoreCopiesOfBook(newBook);

            return RedirectToAction(nameof(Details), new { id });

        }

        // GET: BookDetails/Details/5
        public async Task<IActionResult> Details(int id) //Was int?
        {

            var bookDetails = bookService.GetBookDetailsById(id);
            var displayBookVm = new BookDetailsVm();
            displayBookVm.ID = bookDetails.ID;
            displayBookVm.Author = authorService.GetAuthorById(bookDetails.AuthorID).Name;
            displayBookVm.AuthorID = bookDetails.AuthorID;
            displayBookVm.Titel = bookDetails.Titel;
            displayBookVm.ISBN = bookDetails.ISBN;
            displayBookVm.Details = bookDetails.Details;
            displayBookVm.BookCopies = bookDetails.BookCopies.ToList();
            return View(displayBookVm);
        }
        
        // GET: BookDetails/Create
        public IActionResult Create()
        {
            var vm = new CreateBookVm(); //Create a VM object
            vm.Authors = new SelectList(authorService.GetAllAuthors(), "ID", "Name");
            return View(vm);
        }

        // POST: BookDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookVm vm)
        {
            if (ModelState.IsValid)
            {
                var newBook = new BookDetails();
                newBook.AuthorID = vm.AuthorID;
                newBook.ISBN = vm.ISBN;
                newBook.Titel = vm.Title;
                newBook.Details = vm.Details;

                await Task.Run(() => bookService.AddNewBookDetails(newBook));

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error", "Home", "");
        }

        // GET: BookDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var bookDetails = await Task.Run(()=> bookService.GetBookDetailsById(id)); //bookService.Get //_context.BookDetails.FindAsync(id);
            if (bookDetails == null)
            {
                return NotFound();
            }
            EditBookVm vm = new EditBookVm();
            vm.ISBN = bookDetails.ISBN;
            vm.Titel = bookDetails.Titel;
            vm.Authors = new SelectList(authorService.GetAllAuthors(), "ID", "Name", bookDetails.Author);
            vm.AuthorID = bookDetails.AuthorID;
            vm.Details = bookDetails.Details;
            return View(vm);
           // ViewData["AuthorID"] = new SelectList(authorService.GetAllAuthors(), "ID", "ID", bookDetails.AuthorID);
        }

        // POST: BookDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ISBN,Titel,AuthorID,Details")] EditBookVm vm)
        {
            
            if (!ModelState.IsValid)
                return View(vm);

            var bookDetails = bookService.GetBookDetailsById(id);

            bookDetails.ISBN = vm.ISBN;
            bookDetails.Titel = vm.Titel;
            bookDetails.AuthorID = vm.AuthorID;
            bookService.UpdateBookDetails(bookDetails);

            return RedirectToAction(nameof(Index));
            
        }
        
        
        // GET: BookDetails/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = bookService.GetBookDetailsById(id);
            bookDetails.Author = authorService.GetAuthorById(bookDetails.AuthorID);
            return View(bookDetails);
          
        }
        
        // POST: BookDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bookService.DeleteBookDetailsByID(id);
            return RedirectToAction(nameof(Index));
        }

    } 
}

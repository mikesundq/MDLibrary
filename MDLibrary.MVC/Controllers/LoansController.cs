using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;
using MDLibrary.MVC.Models.LoanVM;
using MDLibrary.Infrastructure.Service;
using MDLibrary.Application.Interfaces;


namespace MDLibrary.MVC.Controllers
{
    public class LoansController : Controller
    {
        private readonly IBookServices bookService;
        private readonly IMemberService memberService;
        private readonly ILoanService loanService;

        public LoansController(ILoanService loanService, IMemberService memberService, IBookServices bookService)
        {
            this.bookService = bookService;
            this.memberService = memberService;
            this.loanService = loanService;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            
            var loans = await Task.Run(() => loanService.GetAllLoans());
            var vm = new LoanIndexVm();
            vm.Loans = loans;
            return View(vm);
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var loan = await Task.Run(() => loanService.GetLoanById(id));
            if (loan == null)
            {
                return NotFound();
            }

            var vm = new DetailsLoanVm();
            vm.LoanBooks = loan.LoanBooks;
            vm.ID = loan.ID;
            vm.Member = loan.Member;
            vm.MemberID = loan.MemberID;
            vm.TimeOfLoan = loan.TimeOfLoan;
            vm.TimeToReturnBook = loan.TimeToReturnBook;
            if (vm.TimeToReturnBook < DateTime.Today && vm.LoanBooks.Count > 0)
            {
                vm.Latefee = loanService.CalculateLateFee(vm.TimeToReturnBook);
            }

            return View(vm);
        }

        // GET: Loans/Create/loanbooks.int[]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateLoanVm();

            vm.Members = new SelectList(memberService.GetAllMembers(), "ID", "Name");
            vm.AvalibleBooks = await Task.Run(() => loanService.ShowAllBooksNotOnLoan());
            return View(vm);
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Get a int[] with bookcopies checked in view
        public async Task<IActionResult> Create(CreateLoanVm vm , int[] loanBooks) 
        {
            if (loanBooks.Count() <= 0)
            {
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                var loan = new Loan();
                loan.MemberID = vm.MemberID;
                loan.TimeOfLoan = Convert.ToDateTime(vm.TimeOfLoan);
                loan.TimeToReturnBook = Convert.ToDateTime(vm.TimeToReturnBook);

                //Get books from bookids in loanbooks
                loan.BookCopies = await Task.Run(() => bookService.GetBookCopiesById(loanBooks));

                //Save
                loanService.LoanOutBook(loan);

                return RedirectToAction(nameof(Index));
            }
            
            return RedirectToAction("Error", "Home", "");
        }

        [Route("{controller}/{action}/{bookCopyID}/{loanID}")]
        //GET Loans/ReturnBook/bookCopyID/loanID
        //return one book from loan
        public async Task<IActionResult> ReturnBook(int bookCopyID, int loanID)
        {

            await Task.Run(() => loanService.ReturnOneBook(bookCopyID));

            //return  LocalRedirect($"")
            return RedirectToAction(nameof(Details), new { id = loanID });
        }

        //GET Loans/ReturnBooks/loanID
        //returns all books in loan by id
        public async Task<IActionResult> ReturnBooks(int id)
        {
            await Task.Run(() => loanService.ReturnAllBooks(id));

            return RedirectToAction(nameof(Details), new { id });
        }

    }
}

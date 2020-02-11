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
            var loans = loanService.GetAllLoans();
            var vm = new LoanIndexVm();
            vm.Loans = loans;
            return View(vm);
        }

        // GET: Loans/Details/5
      /*  public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.BookCopy)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        } */

        // GET: Loans/Create
        public IActionResult Create()
        {
            var vm = new CreateLoanVm();
           
            //vm.TimeOfLoan = new DateTime();
            //ViewData["BookCopyID"] = new SelectList(_context.Book, "ID", "ID");
            //ViewData["MemberID"] = new SelectList(_context.Member, "ID", "Name");
            return View(vm);
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLoanVm vm) //([Bind("ID,TimeOfLoan,TimeToReturnBook,BookCopyID,MemberID")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                var loan = new Loan();
                loan.BookCopyID = vm.BookCopyID;
                loan.MemberID = vm.MemberID;
                loan.TimeOfLoan = vm.TimeOfLoan;
                loan.TimeToReturnBook = Convert.ToDateTime(vm.TimeToReturnBook);

                loanService.LoanOutBook(loan);

                return RedirectToAction(nameof(Index));
                //_context.Add(loan);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["BookCopyID"] = new SelectList(_context.Book, "ID", "ID", loan.BookCopyID);
            //ViewData["MemberID"] = new SelectList(_context.Member, "ID", "ID", loan.MemberID);
            //return View(loan);
            return RedirectToAction("Error", "Home", "");
        }

       /* // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["BookCopyID"] = new SelectList(_context.Book, "ID", "ID", loan.BookCopyID);
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "ID", loan.MemberID);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TimeOfLoan,TimeToReturnBook,BookCopyID,MemberID")] Loan loan)
        {
            if (id != loan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.ID))
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
            ViewData["BookCopyID"] = new SelectList(_context.Book, "ID", "ID", loan.BookCopyID);
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "ID", loan.MemberID);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.BookCopy)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.ID == id);
        } */
    }
}

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
using MDLibrary.MVC.Models.MemberVM;

namespace MDLibrary.MVC.Controllers
{
    public class MembersController : Controller
    {
        private readonly IBookServices bookServices;
        private readonly ILoanService loanService;
        private readonly IMemberService memberService;

        public MembersController(IMemberService memberService, ILoanService loanService, IBookServices bookServices)
        {
            this.bookServices = bookServices;
            this.loanService = loanService;
            this.memberService = memberService;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var vm = new MemberIndexVm();
            vm.Members = await Task.Run(() => memberService.GetAllMembers());
            return View(vm); 
        }


        
        // GET: Members/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vm = new DetailsMemberVm();

            var member = await Task.Run(() => memberService.GetMemberById(id));
            if (member == null)
                return NotFound();

            vm.ID = member.ID;
            vm.Name = member.Name;
            vm.CanBeRemoved = await Task.Run(() => memberService.CanRemoveMember(id));

            if (member.Loans != null)
                vm.Loans = await Task.Run(() => loanService.ShowAllLoansByMember(vm.ID));

            return View(vm);
        }
        
        // GET: Members/Create
        public IActionResult Create()
        {
            var vm = new CreateMemberVm();
            return View(vm);
        }
        
        

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var newMemberToAdd = new Member();
            newMemberToAdd.Name = vm.Name;
            newMemberToAdd.SSN = vm.SSN;
            await Task.Run(() => memberService.AddNewMember(newMemberToAdd));
            
            return RedirectToAction(nameof(Index));
            
        }
        
        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var memberToEdit = await Task.Run(() => memberService.GetMemberById(id));
            if (memberToEdit == null)
            {
                return NotFound();
            }
            var vm = new EditMemberVm();
            vm.Name = memberToEdit.Name;
            vm.SSN = memberToEdit.SSN;

            return View(vm);
        }
        
        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, SSN,Name")] EditMemberVm vm)
        {

            if (!ModelState.IsValid)
                return View(vm);

            var member = await Task.Run(()=> memberService.GetMemberById(id));
            member.SSN = vm.SSN;
            member.Name = vm.Name;
            memberService.EditMember(member);


            return RedirectToAction(nameof(Index));
        }
        
        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var member = await Task.Run(()=> memberService.GetMemberById(id));
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(()=> memberService.RemoveMemberById(id));
            return RedirectToAction(nameof(Index));
        }
       
    }
}

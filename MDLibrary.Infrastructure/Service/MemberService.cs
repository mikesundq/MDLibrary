using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MDLibrary.Infrastructure.Service
{
    public class MemberService : IMemberService
    {

        private readonly ApplicationDbContext context;

        public MemberService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void AddNewMember(Member member)
        {
            context.Member.Add(member);
            context.SaveChanges();
        }

        public void EditMember(Member member)
        {
            context.Member.Update(member);
            context.SaveChanges();
        }

        public IList<Member> GetAllMembers()
        {
            return context.Member.Include(m => m.Loans).OrderBy(m => m.Name).ToList();
            //return context.Member.OrderBy(m => m.Name).ToList();
        }

        public Member GetMemberById(int id)
        {
            //try to get loans.bookcopies...
            

            return context.Member
                .Include(m => m.Loans)
                .FirstOrDefault(m => m.ID == id);

            //return context.Member.FindAsync(id).Result;
        }

        public void RemoveMemberById(int id)
        {
            var member = context.Member.Find(id);
            context.Member.Remove(member);
            context.SaveChanges();
        }

        public bool CanRemoveMember(int id)
        {
            var member = context.Member.Include(m => m.Loans)
                .FirstOrDefault(a => a.ID == id);

            return member.Loans.Count < 1;
            //return context.Author.Contains()
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;

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

        public IList<Member> GetAllMembers()
        {
            return context.Member.OrderBy(m => m.Name).ToList();
        }
       
    }
}

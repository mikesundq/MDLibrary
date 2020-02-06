using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MDLibrary.Domain;

namespace MDLibrary.Application.Interfaces
{
    public interface IMemberService
    {
        public void AddNewMember(Member member);
        Task<object> ToListAsync(); //What is this?? do we need it?
        public IList<Member> GetAllMembers();
    }
}

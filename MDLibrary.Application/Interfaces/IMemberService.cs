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
        
        public IList<Member> GetAllMembers();
    }
}

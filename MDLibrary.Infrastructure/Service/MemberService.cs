using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;

namespace MDLibrary.Infrastructure.Service
{
    public class MemberService : IMemberService
    {
        public List<Member> Members { get; set; } = new List<Member>();

        public void AddNewMember(Member member)
        {
            Members.Add(member);
        }
    }
}

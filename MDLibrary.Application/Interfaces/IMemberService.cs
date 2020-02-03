using System;
using System.Collections.Generic;
using System.Text;
using MDLibrary.Domain;

namespace MDLibrary.Application.Interfaces
{
    public interface IMemberService
    {
        public void AddNewMember(Member member);
    }
}

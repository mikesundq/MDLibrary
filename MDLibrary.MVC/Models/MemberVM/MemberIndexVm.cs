using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDLibrary.MVC.Models.MemberVM
{
    public class MemberIndexVm
    {
        public IList<Member> Members { get; set; } = new List<Member>();
    }
}

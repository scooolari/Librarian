using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reports
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BooksBorrowed { get; set; }
    }
}

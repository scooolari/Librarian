using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Context
    {
        public static LibrarianEntities sLibrarianEntities;

        public static LibrarianEntities GetContext()
        {
            sLibrarianEntities = new LibrarianEntities();
            return sLibrarianEntities;
        }
    }
}

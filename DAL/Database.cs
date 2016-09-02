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
        public static LibrarianEntities LibrarianEntity;

        public static LibrarianEntities GetContext()
        {
            LibrarianEntity = new LibrarianEntities();
            return LibrarianEntity;
        }
    }
}

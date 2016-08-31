using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Books
{
    /// <summary>
    /// Model to view books list
    /// </summary>
    public class BooksViewModel
    {
        public List<BookViewModel> Books { get; set; }
    }
}

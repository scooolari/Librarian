using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Borrows
{
    public class UserBookViewModel
    {
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}

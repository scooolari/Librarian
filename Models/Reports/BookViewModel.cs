using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reports
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int BookGenreId { get; set; }
        public string BookGenre { get; set; }
        public int Count { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}

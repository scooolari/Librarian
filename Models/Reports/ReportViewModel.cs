using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reports
{
    public class ReportViewModel
    {
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<BorrowViewModel> BooksList { get; set; }
        public List<UserViewModel> UsersList { get; set; }
        public string BookGenre { get; set; }
        public List<KeyValuePair<int, string>> DictBookGenreList { get; set; }
    }
}

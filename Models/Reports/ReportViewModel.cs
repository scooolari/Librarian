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
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<BorrowViewModel> BooksList { get; set; }
        public List<UserViewModel> UsersList { get; set; }
        public int? BookGenreId { get; set; }
        public List<KeyValuePair<int, string>> DictBookGenreList { get; set; }
    }
}

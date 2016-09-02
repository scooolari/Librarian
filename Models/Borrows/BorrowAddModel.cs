using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Borrows
{
    public class BorrowAddModel
    {
        public List<KeyValuePair<int, string>> UsersList { get; set; }
        public int SelectedUserId { get; set; }
        public List<KeyValuePair<int, string>> BooksList { get; set; }
        public int[] ChosenBooks { get; set; }
    }
}

using DAL;
using Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ReportService
{
    public class ReportService
    {
        public List<BorrowViewModel> GetBooksBorrowsList(int? bookGenreId, string title, string fromDate, string toDate)
        {
            title = string.IsNullOrWhiteSpace(title) ? "" : title;
            DateTime dateFrom, dateTo;
            bool fromDateValid = DateTime.TryParse(fromDate, out dateFrom);
            bool toDateVaild = DateTime.TryParse(toDate, out dateTo);

            List<BorrowViewModel> borrowsViewModelList = Context.GetContext().Borrow
                .Where(a => (bookGenreId.HasValue ? a.Book.BookGenreId.Equals(bookGenreId.Value) : true) 
                    && (title == "" ? true : a.Book.Title.Contains(title))
                    && (fromDateValid ? a.FromDate >= dateFrom : true)
                    && (toDateVaild ? a.ToDate <= dateTo : true))
                .Select(item => new BorrowViewModel
                {
                    BorrowId = item.BorrowId,
                    BookId = item.BookId,
                    Author = item.Book.Author,
                    Title = item.Book.Title,
                    BookGenreId = item.Book.BookGenreId,
                    BookGenre = item.Book.DictBookGenre.Name,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate
                }).ToList();

            return borrowsViewModelList;
        }

        public List<BorrowViewModel> GetBooksBorrowsList()
        {
            List<BorrowViewModel> bookViewModelList = Context.GetContext().Borrow
                .Select(item => new BorrowViewModel
                {
                    BorrowId = item.BorrowId,
                    BookId = item.BookId,
                    Author = item.Book.Author,
                    Title = item.Book.Title,
                    BookGenreId = item.Book.BookGenreId,
                    BookGenre = item.Book.DictBookGenre.Name,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate
                }).ToList();

            return bookViewModelList;
        }

        public List<KeyValuePair<int, string>> GetDictBookGenreList()
        {
            Context.GetContext();
            List<KeyValuePair<int, string>> dictBookGenreList =
                Context.LibrarianEntity.DictBookGenre.ToDictionary(a => a.BookGenreId, a => a.Name).ToList();

            return dictBookGenreList;
        }

        public List<UserViewModel> GetUsersList()
        {
            List<UserViewModel> usersList = Context.GetContext().User.Select(item => new UserViewModel
            {
                UserId = item.UserId,
                UserName = string.Concat(item.FirstName, " ", item.LastName),
                BooksBorrowed = item.Borrow.Count(a => !a.IsReturned)
            }).OrderByDescending(a => a.BooksBorrowed).ToList();

            return usersList;
        }
    }
}

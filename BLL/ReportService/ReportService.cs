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
        public List<BookViewModel> GetBooksBorrowsList(string bookGenre, string title, DateTime fromDate, DateTime toDate)
        {
            bookGenre = bookGenre.Trim();
            string[] bookGenres = bookGenre.Split(',');

            List<BookViewModel> bookViewModelList = Context.GetContext().Borrow
                .Where(a => (bookGenre == "" ? true : (bookGenres.Contains(a.Book.DictBookGenre.Name))) 
                    && (title == "" ? true : a.Book.Title.Contains(title))
                    && (a.FromDate >= fromDate || a.ToDate <= toDate))
                .Select(item => new BookViewModel
                {
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

        public List<BookViewModel> GetBooksBorrowsList()
        {
            List<BookViewModel> bookViewModelList = Context.GetContext().Borrow
                .Select(item => new BookViewModel
                {
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
            }).ToList();

            return usersList;
        }
    }
}

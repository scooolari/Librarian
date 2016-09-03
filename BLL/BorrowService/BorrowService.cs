using DAL;
using Models.Borrows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BorrowService
{
    public class BorrowService
    {
        public IEnumerable<BorrowViewModel> GetBorrowsList()
        {
            IEnumerable<BorrowViewModel> borrowViewModelEnumerable = Context.GetContext().Borrow.Select(item => new BorrowViewModel
            {
                BorrowId = item.BorrowId,
                UserId = item.UserId,
                UserName = string.Concat(item.User.FirstName, " ", item.User.LastName),
                BookId = item.BookId,
                BookTitle = item.Book.Title,
                FromDate = item.FromDate,
                ToDate = item.ToDate,
                IsReturned = item.IsReturned,
                IsReturnedString = item.IsReturned ? "Yes" : "No"
            });
            return borrowViewModelEnumerable;
        }

        public List<KeyValuePair<int, string>> GetAvailableBooksList()
        {
            Context.GetContext();
            List<KeyValuePair<int, string>> availableBooksList = Context.LibrarianEntity.Book
                .Where(a => (a.Count - a.Borrow.Count(b => !b.IsReturned)) > 0)
                .ToDictionary(a => a.BookId, a => a.Title).ToList();
            return availableBooksList;
        }

        public List<KeyValuePair<int, string>> GetUsersList()
        {
            Context.GetContext();
            List<KeyValuePair<int, string>> usersList = Context.LibrarianEntity.User.OrderBy(a => a.LastName).ThenBy(a => a.FirstName)
                .ToDictionary(a => a.UserId, a => string.Concat(a.FirstName, " ", a.LastName)).ToList();
            return usersList;
        }

        public void AddBorrows(int userId, int[] booksIds)
        {
            Context.GetContext();
            DateTime from = DateTime.Now;
            DateTime to = from.AddMonths(1);
            for (int i = 0; i < booksIds.Length; i++)
            {
                Borrow newBorrow = new Borrow();
                newBorrow.UserId = userId;
                newBorrow.BookId = booksIds[i];
                newBorrow.FromDate = from;
                newBorrow.ToDate = to;
                newBorrow.IsReturned = false;
                Context.LibrarianEntity.Borrow.Add(newBorrow);
            }
            Context.LibrarianEntity.SaveChanges();
        }

        public void ReturnBook(int borrowId)
        {
            Context.GetContext();
            Borrow returnedBorrow = Context.LibrarianEntity.Borrow.FirstOrDefault(a => a.BorrowId == borrowId);
            if (returnedBorrow != null)
            {
                returnedBorrow.IsReturned = true;
                Context.LibrarianEntity.SaveChanges();
            }
        }

        public IEnumerable<UserWithBooksViewModel> GetUsersWithBooksList()
        {
            IEnumerable<UserWithBooksViewModel> usersWithBooksList = Context.GetContext().User
                .Select(item => new UserWithBooksViewModel
                {
                    UserId = item.UserId,
                    UserName = string.Concat(item.FirstName, " ", item.LastName),
                    BooksBorrowed = item.Borrow.Count(a => !a.IsReturned)
                })
                .Where(a => a.BooksBorrowed > 0);
            return usersWithBooksList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Books;
using DAL;

namespace BLL.BookService
{
    public class BookService
    {
        public BooksViewModel GetBooksViewModel()
        {
            BooksViewModel lBooksViewModel = new BooksViewModel();
            lBooksViewModel.Books = GetBookViewModelEnumerable().ToList();
            return lBooksViewModel;
        }

        public IEnumerable<BookViewModel> GetBookViewModelEnumerable()
        {
            return Context.GetContext().Book.Select(item => new BookViewModel
            {
                BookId = item.BookId,
                Author = item.Author,
                Title = item.Title,
                ReleaseDate = item.ReleaseDate,
                ISBN = item.ISBN,
                BookGenreId = item.BookGenreId,
                BookGenre = item.DictBookGenre.Name,
                Count = item.Count,
                AddDate = item.AddDate,
                ModifiedDate = item.ModifiedDate
            });
        }

        public BookViewModel GetBookViewModel(int aBookId)
        {
            BookViewModel lBookViewModel = new BookViewModel();
            Book lBook = Context.GetContext().Book.FirstOrDefault(a => a.BookId == aBookId);
            if (lBook != null)
                Copy.CopyPropertyValues(lBook, lBookViewModel);
            return lBookViewModel;
        }

        public BookViewModel AddBook(BookViewModel aBookViewModel)
        {
            Context.GetContext();
            Book lNewBook = new Book()
            {
                Author = aBookViewModel.Author,
                Title = aBookViewModel.Title,
                ReleaseDate = aBookViewModel.ReleaseDate,
                ISBN = aBookViewModel.ISBN,
                BookGenreId = aBookViewModel.BookGenreId,
                Count = aBookViewModel.Count,
                AddDate = DateTime.Now
            };
            Context.sLibrarianEntities.Book.Add(lNewBook);
            Context.sLibrarianEntities.SaveChanges();
            aBookViewModel.BookId = lNewBook.BookId;
            return aBookViewModel;
        }

        public BookViewModel EditBook(BookViewModel aBookViewModel)
        {
            if (aBookViewModel.BookId > 0)
            {
                Context.GetContext();
                Book lEditedBook = Context.sLibrarianEntities.Book.FirstOrDefault(a => a.BookId == aBookViewModel.BookId);
                if (lEditedBook != null)
                {
                    lEditedBook.Author = aBookViewModel.Author;
                    lEditedBook.Title = aBookViewModel.Title;
                    lEditedBook.ReleaseDate = aBookViewModel.ReleaseDate;
                    lEditedBook.ISBN = aBookViewModel.ISBN;
                    lEditedBook.BookGenreId = aBookViewModel.BookGenreId;
                    lEditedBook.Count = aBookViewModel.Count;
                    lEditedBook.ModifiedDate = DateTime.Now;
                    Context.sLibrarianEntities.SaveChanges();
                    return aBookViewModel;
                }
            }
            return null;
        }
    }
}

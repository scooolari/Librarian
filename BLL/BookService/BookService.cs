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
            BooksViewModel booksViewModel = new BooksViewModel();
            booksViewModel.Books = GetBookViewModelEnumerable().ToList();
            return booksViewModel;
        }

        public IEnumerable<BookViewModel> GetBookViewModelEnumerable()
        {
            IEnumerable<BookViewModel> bookViewModelEnumerable = Context.GetContext().Book.Select(item => new BookViewModel
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
            return bookViewModelEnumerable;
        }

        public BookViewModel GetBookViewModel(int bookId)
        {
            BookViewModel bookViewModel = new BookViewModel();
            Book bookEntity = Context.GetContext().Book.FirstOrDefault(a => a.BookId == bookId);
            
            if (bookEntity != null)
                Copy.CopyPropertyValues(bookEntity, bookViewModel);
            
            bookViewModel.DictBookGenreList = GetDictBookGenreList();
            
            return bookViewModel;
        }

        public BookViewModel AddBook(BookViewModel bookViewModel)
        {
            Context.GetContext();
            Book newBook = new Book()
            {
                Author = bookViewModel.Author,
                Title = bookViewModel.Title,
                ReleaseDate = bookViewModel.ReleaseDate,
                ISBN = bookViewModel.ISBN,
                BookGenreId = bookViewModel.BookGenreId,
                Count = (bookViewModel.Count < 1 ? 1 : bookViewModel.Count),
                AddDate = DateTime.Now
            };
            Context.LibrarianEntity.Book.Add(newBook);
            Context.LibrarianEntity.SaveChanges();
            bookViewModel.BookId = newBook.BookId;
            return bookViewModel;
        }

        public BookViewModel EditBook(BookViewModel bookViewModel)
        {
            if (bookViewModel.BookId > 0)
            {
                Context.GetContext();
                Book editedBook = Context.LibrarianEntity.Book.FirstOrDefault(a => a.BookId == bookViewModel.BookId);
                if (editedBook != null)
                {
                    editedBook.Author = bookViewModel.Author;
                    editedBook.Title = bookViewModel.Title;
                    editedBook.ReleaseDate = bookViewModel.ReleaseDate;
                    editedBook.ISBN = bookViewModel.ISBN;
                    editedBook.BookGenreId = bookViewModel.BookGenreId;
                    editedBook.Count = (bookViewModel.Count < 0 ? 0 : bookViewModel.Count);
                    editedBook.ModifiedDate = DateTime.Now;

                    Context.LibrarianEntity.SaveChanges();
                    
                    return bookViewModel;
                }
            }
            return null;
        }

        public List<DictBookGenreViewModel> GetDictBookGenreList()
        {
            Context.GetContext();
            List<DictBookGenreViewModel> dictBookGenreViewModelsList =
                Context.LibrarianEntity.DictBookGenre.Select(item => new DictBookGenreViewModel
                {
                    BookGenreId = item.BookGenreId,
                    Name = item.Name
                }).ToList();
            return dictBookGenreViewModelsList;
        }
    }
}

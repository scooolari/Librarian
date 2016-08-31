using BLL.BookService;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Books;

namespace Librarian.Controllers
{
    public class BooksController : Controller
    {
        //
        // GET: /Books/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBooksList([DataSourceRequest]DataSourceRequest request)
        {
            BookService lBookService = new BookService();
            return Json(lBookService.GetBookViewModelEnumerable().ToDataSourceResult(request));
        }

        //public ActionResult AddBookIndex(UserViewModel aUserViewModel)
        //{
        //    ModelState.Clear();
        //    return View("AddUser");
        //}

        //public ActionResult AddBook(UserViewModel aUserViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UserService lUserService = new UserService();
        //        lUserService.AddUser(aUserViewModel);
        //        return View("Index", lUserService.GetUsersViewModel());
        //    }
        //    else
        //        return View("AddUser");
        //}

        //public ActionResult EditBookIndex(int aBookId)
        //{
        //    BookService lBookService = new BookService();
        //    BookViewModel lBookViewModel = lBookService.GetBookViewModel(aBookId);
        //    if (lBookViewModel.BookId > 0)
        //        return View("EditBook", lBookViewModel);
        //    else
        //        return View("AddUser");
        //}

        public ActionResult AddEditBookIndex(int aBookId)
        {
            if (aBookId > 0)
                return View("EditBook");
            else
                return View("AddBook");
            
        }

        public ActionResult EditBook(BookViewModel aBookViewModel)
        {
            BookService lBookService = new BookService();
            if (aBookViewModel.BookId > 0)
                return Json(lBookService.EditBook(aBookViewModel));
            else
                return Json(lBookService.AddBook(aBookViewModel));
        }
    }
}
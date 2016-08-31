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

        public ActionResult AddBookIndex(BookViewModel aBookViewModel)
        {
            ModelState.Clear();
            return View("AddBook");
        }

        public ActionResult AddBook(BookViewModel aBookViewModel)
        {
            if (ModelState.IsValid)
            {
                BookService lBookService = new BookService();
                lBookService.AddBook(aBookViewModel);
                if (Request.IsAjaxRequest())
                    return Json(new { result = true });
            }
            return Json(new { result = false });
        }

        public ActionResult EditBookIndex(BookViewModel aBookViewModel)
        {
            if (aBookViewModel.BookId > 0)
                return View("EditBook", aBookViewModel);
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
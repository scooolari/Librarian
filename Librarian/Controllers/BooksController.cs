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

        [HttpGet]
        public ActionResult AddBook()
        {
            ModelState.Clear();
            BookService lBookService = new BookService();
            BookViewModel lBookViewModel = new BookViewModel();
            lBookViewModel.DictBookGenreList = lBookService.GetDictBookGenreList();
            return PartialView("AddBook", lBookViewModel);
        }

        [HttpPost]
        public ActionResult AddBook(BookViewModel aBookViewModel)
        {
            if (ModelState.IsValid)
            {
                BookService lBookService = new BookService();
                lBookService.AddBook(aBookViewModel);
            }
            return PartialView("AddBook", aBookViewModel);
        }

        [HttpGet]
        public ActionResult EditBook(int aBookId)
        {
            BookService lBookService = new BookService();
            BookViewModel lBookViewModel = lBookService.GetBookViewModel(aBookId);
            return PartialView("EditBook", lBookViewModel);
        }

        [HttpPost]
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
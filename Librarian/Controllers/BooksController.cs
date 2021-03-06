﻿using BLL.BookService;
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
            DataSourceResult bookViewModelDataSourceResults = lBookService.GetBookViewModelEnumerable().ToDataSourceResult(request);
            return Json(bookViewModelDataSourceResults);
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
            BookService lBookService = new BookService();
            if (ModelState.IsValid)
            {
                lBookService.AddBook(aBookViewModel);
                return Json(new { IsValid = true, Mode = "Add" });
            }
            else
                aBookViewModel.DictBookGenreList = lBookService.GetDictBookGenreList();
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
            if (ModelState.IsValid)
            {
                lBookService.EditBook(aBookViewModel);
                return Json(new { IsValid = true, Mode = "Edit" });
            }
            else
                aBookViewModel.DictBookGenreList = lBookService.GetDictBookGenreList();
            return PartialView("EditBook", aBookViewModel);
        }
    }
}
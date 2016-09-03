using BLL.BorrowService;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Borrows;
using BLL.UserService;
using Models.Users;

namespace Librarian.Controllers
{
    public class BorrowsController : Controller
    {
        //
        // GET: /Borrows/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBorrowsList([DataSourceRequest]DataSourceRequest request)
        {
            BorrowService borrowService = new BorrowService();
            DataSourceResult borrowViewModelDataSourceResults = borrowService.GetBorrowsList().ToDataSourceResult(request);
            return Json(borrowViewModelDataSourceResults);
        }

        [HttpGet]
        public ActionResult AddBorrow()
        {
            ModelState.Clear();
            BorrowService borrowService = new BorrowService();
            BorrowAddModel borrowAddModel = new BorrowAddModel();
            borrowAddModel.UsersList = borrowService.GetUsersList();
            borrowAddModel.BooksList = borrowService.GetAvailableBooksList();
            return PartialView("AddBorrow", borrowAddModel);
        }

        [HttpPost]
        public ActionResult AddBorrow(BorrowAddModel borrowAddModel)
        {
            BorrowService borrowService = new BorrowService();
            borrowService.AddBorrows(borrowAddModel.SelectedUserId, borrowAddModel.ChosenBooks);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Borrows");
            return Json(new { Url = redirectUrl });
        }

        [HttpGet]
        public ActionResult ReturnBook(int borrowId)
        {
            BorrowService borrowService = new BorrowService();
            borrowService.ReturnBook(borrowId);
            return View("Index");
        }

        [HttpPost]
        public ActionResult ReturnBooks(int[] borrowsIds, int userId)
        {
            BorrowService borrowService = new BorrowService();
            borrowService.ReturnBooks(borrowsIds);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("UserBooks", "Borrows", userId);
            return Json(new { Url = string.Concat(redirectUrl, "?userId=", userId) });
        }

        public ActionResult GetUsersWithBooksList([DataSourceRequest]DataSourceRequest request)
        {
            BorrowService borrowService = new BorrowService();
            DataSourceResult usersWithBooksDataSourceResults = borrowService.GetUsersWithBooksList().ToDataSourceResult(request);
            return Json(usersWithBooksDataSourceResults);
        }

        [HttpGet]
        public ActionResult UserBooks(int userId)
        {
            UserService userService = new UserService();
            UserViewModel userViewModel = userService.GetUserViewModel(userId);
            ViewBag.Header = string.Concat(userViewModel.FirstName, " ", userViewModel.LastName, " books");
            
            return View(userViewModel);
        }

        public ActionResult GetUserBooksList([DataSourceRequest]DataSourceRequest request, int userId)
        {
            BorrowService borrowService = new BorrowService();
            DataSourceResult userBooksList = borrowService.GetUserBooksList(userId).ToDataSourceResult(request);
            return Json(userBooksList);
        }
    }
}
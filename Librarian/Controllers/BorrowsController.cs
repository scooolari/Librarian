using BLL.BorrowService;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Borrows;

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
	}
}
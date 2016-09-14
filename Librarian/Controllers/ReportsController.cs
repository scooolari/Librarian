using BLL.ReportService;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Librarian.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            ReportService reportService = new ReportService();

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.BookGenre = string.Empty;
            reportViewModel.DictBookGenreList = reportService.GetDictBookGenreList();
            reportViewModel.Title = string.Empty;
            reportViewModel.BooksList = reportService.GetBooksBorrowsList(reportViewModel.BookGenre, reportViewModel.Title);
            reportViewModel.UsersList = reportService.GetUsersList();

            return View(reportViewModel);
        }

        public ActionResult BooksGridPartial(ReportViewModel reportViewModel)
        {
            return PartialView(reportViewModel);
        }

        public ActionResult UsersGridPartial(ReportViewModel reportViewModel)
        {
            return PartialView(reportViewModel);
        }

        public ActionResult GetBorrowsList([DataSourceRequest]DataSourceRequest request)
        {
            ReportService reportService = new ReportService();
            DataSourceResult booksListDataSourceResults = reportService.GetBooksBorrowsList().ToDataSourceResult(request);
            return Json(booksListDataSourceResults, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMostActiveUsersList([DataSourceRequest]DataSourceRequest request)
        {
            ReportService reportService = new ReportService();
            DataSourceResult usersListDataSourceResults = reportService.GetUsersList().ToDataSourceResult(request);
            return Json(usersListDataSourceResults, JsonRequestBehavior.AllowGet);
        }
    }
}
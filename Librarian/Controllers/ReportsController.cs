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
        #region Actions

        // GET: Reports
        public ActionResult Index()
        {
            ReportViewModel reportViewModel = SetDefaultReportViewModel();
            return View(reportViewModel);
        }

        [HttpGet]
        public ActionResult BooksGridPartial(ReportViewModel reportViewModel)
        {
            return PartialView(reportViewModel);
        }

        [HttpPost]
        public ActionResult FilterBooksGridPartial(ReportViewModel reportViewModel)
        {
            ReportService reportService = new ReportService();
            reportViewModel.BooksList = reportService.GetBooksBorrowsList(reportViewModel.BookGenreId, reportViewModel.Title, reportViewModel.FromDate, reportViewModel.ToDate);
            return PartialView("BooksGridPartial", reportViewModel);
        }

        [HttpGet]
        public ActionResult ResetBooksGridPartial()
        {
            return PartialView();
        }

        public ActionResult UsersGridPartial(ReportViewModel reportViewModel)
        {
            return PartialView(reportViewModel);
        }

        public ActionResult GetBorrowsList([DataSourceRequest]DataSourceRequest request, int? bookGenreId, string title, string fromDate, string toDate)
        {
            ReportService reportService = new ReportService();
            DataSourceResult booksListDataSourceResults = reportService.GetBooksBorrowsList(bookGenreId, title, fromDate, toDate).ToDataSourceResult(request);
            return Json(booksListDataSourceResults, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMostActiveUsersList([DataSourceRequest]DataSourceRequest request)
        {
            ReportService reportService = new ReportService();
            DataSourceResult usersListDataSourceResults = reportService.GetUsersList().ToDataSourceResult(request);
            return Json(usersListDataSourceResults, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private methods

        private ReportViewModel SetDefaultReportViewModel()
        {
            ReportService reportService = new ReportService();

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.DictBookGenreList = reportService.GetDictBookGenreList();
            reportViewModel.BooksList = reportService.GetBooksBorrowsList(reportViewModel.BookGenreId, reportViewModel.Title, reportViewModel.FromDate, reportViewModel.ToDate);
            reportViewModel.UsersList = reportService.GetUsersList();

            return reportViewModel;
        }

        #endregion
    }
}
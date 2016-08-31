using BLL.UserService;
using Models;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Librarian.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        public ActionResult Index()
        {
            UserService lUserService = new UserService();
            return View(lUserService.GetUsersViewModel());
        }

        public ActionResult AddUserIndex(UserViewModel aUserViewModel)
        {
            ModelState.Clear();
            return View("AddUser");
        }

        public ActionResult AddUser(UserViewModel aUserViewModel)
        {
            if (ModelState.IsValid)
            {
                UserService lUserService = new UserService();
                lUserService.AddUser(aUserViewModel);
                return View("Index", lUserService.GetUsersViewModel());
            }
            else
                return View("AddUser");
        }

        public ActionResult EditUserIndex(int aUserId)
        {
            UserService lUserService = new UserService();
            UserViewModel lUserViewModel = lUserService.GetUserViewModel(aUserId);
            if (lUserViewModel.UserId > 0)
                return View("EditUser", lUserViewModel);
            else
                return View("AddUser");
        }

        public ActionResult EditUser(UserViewModel aUserViewModel)
        {
            if (aUserViewModel.UserId > 0)
            {
                UserService lUserService = new UserService();
                lUserService.EditUser(aUserViewModel);
                return View("Index", lUserService.GetUsersViewModel());
            }
            else
                return View("EditUser", aUserViewModel);
        }

        public ActionResult DeleteUser(int aUserId)
        {
            UserService lUserService = new UserService();
            lUserService.DeleteUser(aUserId);
            return View("Index", lUserService.GetUsersViewModel());
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using Models.Users;

namespace BLL.UserService
{
    public class UserService
    {
        public UsersViewModel GetUsersViewModel()
        {
            UsersViewModel lUsersViewModel = new UsersViewModel();
            lUsersViewModel.Users = Context.GetContext().User.Select(item => new UserViewModel
            {
                UserId = item.UserId,
                FirstName = item.FirstName,
                LastName = item.LastName,
                BirthDate = item.BirthDate,
                Email = item.Email,
                Phone = item.Phone,
                AddDate = item.AddDate,
                Modified = item.ModifiedDate,
                BooksBorrowed = item.Borrow.Count(a => !a.IsReturned),
                IsActive = item.IsActive,
                IsActiveString = item.IsActive ? "Yes" : "No"
            }).ToList();
            return lUsersViewModel;
        }

        public UserViewModel GetUserViewModel(int aUserId)
        {
            UserViewModel lUserViewModel = new UserViewModel();
            User lUser = Context.GetContext().User.FirstOrDefault(a => a.UserId == aUserId);
            if (lUser != null)
                Copy.CopyPropertyValues(lUser, lUserViewModel);
            return lUserViewModel;
        }

        public int AddUser(UserViewModel aUserViewModel)
        {
            Context.GetContext();
            User lNewUser = new User()
                {
                    FirstName = aUserViewModel.FirstName,
                    LastName = aUserViewModel.LastName,
                    BirthDate = aUserViewModel.BirthDate,
                    Email = aUserViewModel.Email,
                    Phone = aUserViewModel.Phone,
                    IsActive = aUserViewModel.IsActive,
                    AddDate = DateTime.Now
                };
            Context.sLibrarianEntities.User.Add(lNewUser);
            Context.sLibrarianEntities.SaveChanges();
            return lNewUser.UserId;
        }

        public void EditUser(UserViewModel aUserViewModel)
        {
            if (aUserViewModel.UserId > 0)
            {
                Context.GetContext();
                User lEditedUser = Context.sLibrarianEntities.User.FirstOrDefault(a => a.UserId == aUserViewModel.UserId);
                if (lEditedUser != null)
                {
                    lEditedUser.FirstName = aUserViewModel.FirstName;
                    lEditedUser.LastName = aUserViewModel.LastName;
                    lEditedUser.BirthDate = aUserViewModel.BirthDate;
                    lEditedUser.Email = aUserViewModel.Email;
                    lEditedUser.Phone = aUserViewModel.Phone;
                    lEditedUser.IsActive = aUserViewModel.IsActive;
                    lEditedUser.ModifiedDate = DateTime.Now;
                    Context.sLibrarianEntities.SaveChanges();
                }
            }
        }

        public void DeleteUser(int aUserId)
        {
            Context.GetContext();
            User lDeletedUser = Context.sLibrarianEntities.User.FirstOrDefault(a => a.UserId == aUserId);
            Context.sLibrarianEntities.User.Remove(lDeletedUser);
            Context.sLibrarianEntities.SaveChanges();
        }
    }
}

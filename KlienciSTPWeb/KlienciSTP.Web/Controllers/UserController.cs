using KlienciSTP.Data.Model;
using KlienciSTP.Interfaces;
using KlienciSTP.Services;
using KlienciSTP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KlienciSTP.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        // GET: User
        public ActionResult Index()
        {
            var userList = _userService.GetUsers();
            var usersModel = new List<UserViewModel>();

            foreach (var user in userList)
            {
                var model = new UserViewModel(user);
                usersModel.Add(model);
            }

            return View(usersModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var i = new UserViewModel(_userService.GetUser(id));
            return View(i);
        }

        public ActionResult Edit(int id)
        {
            var i = new UserViewModel(_userService.GetUser(id));
            return View(i);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone1 = model.Phone1,
                Phone2 = model.Phone2,
                Email = model.Email,
            };

            _userService.CreateUser(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone1 = model.Phone1,
                Phone2 = model.Phone2,
                Email = model.Email,
            };
            _userService.EditUser(user);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
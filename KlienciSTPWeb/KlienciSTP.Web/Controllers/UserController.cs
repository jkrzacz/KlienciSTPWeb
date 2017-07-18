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
    }
}
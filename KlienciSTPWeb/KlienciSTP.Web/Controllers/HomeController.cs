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
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICarService _carService;

        public HomeController()
        {
            _userService = new UserService();
            _carService = new CarService();
        }
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userList = _userService.GetUsers();
            var usersModel = new List<UserWithCarRowViewModel>();
            foreach (var user in userList)
            {
                foreach (var car in user.Car)
                {
                    if (car.Deleted == null)
                    {
                        var model = new UserWithCarRowViewModel()
                        {
                            UserId = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            CarId = car.Id,
                            Make = car.Make,
                            Model = car.Model,
                            RegistrationNumber = car.RegistrationNumber
                        };
                        usersModel.Add(model);
                    }
                }
            }
            ViewBag.SortOrder = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.MakeSortParm = sortOrder == "Make" ? "Make_desc" : "Make";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.RegistrationNumberSortParm = sortOrder == "RegistrationNumber" ? "RegistrationNumber_desc" : "RegistrationNumber";

            var users = from user in usersModel
                           select user;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(user => user.FirstName.Contains(searchString)
                                         || user.LastName.Contains(searchString)
                                         || user.Make.Contains(searchString)
                                         || user.Model.Contains(searchString)
                                         || user.RegistrationNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "FirstName_desc":
                    users = users.OrderByDescending(user => user.FirstName);
                    break;
                case "LastName":
                    users = users.OrderBy(user => user.LastName);
                    break;
                case "LastName_desc":
                    users = users.OrderByDescending(user => user.LastName);
                    break;
                case "Make":
                    users = users.OrderBy(user => user.Make);
                    break;
                case "Make_desc":
                    users = users.OrderByDescending(user => user.Make);
                    break;
                case "Model":
                    users = users.OrderBy(user => user.Model);
                    break;
                case "Model_desc":
                    users = users.OrderByDescending(user => user.Model);
                    break;
                case "RegistrationNumber":
                    users = users.OrderBy(user => user.RegistrationNumber);
                    break;
                case "RegistrationNumber_desc":
                    users = users.OrderByDescending(user => user.RegistrationNumber);
                    break;
                default:
                    users = users.OrderBy(user => user.FirstName);
                    break;
            }
            return View(users.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
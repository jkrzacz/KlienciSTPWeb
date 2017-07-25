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
        public ActionResult Index()
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
            return View(usersModel);
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
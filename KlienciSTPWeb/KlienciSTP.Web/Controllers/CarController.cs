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
    public class CarController : Controller
    {
        private readonly ICarService _carService;


        public CarController()
        {
            _carService = new CarService();
        }

        public ActionResult Create(int userId)
        {
            var carViewModel = new CarViewModel() {  UserId = userId };
            return PartialView("_Create", carViewModel);
        }


        [HttpPost]
        public ActionResult Create(CarViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", viewModel);
            }

            var car = new Car()
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                Make = viewModel.Make,
                Model = viewModel.Model,
                RegistrationNumber = viewModel.RegistrationNumber,
            };

            _carService.CreateCarForUser(car);
            return Json(new { success = true });
        }

        public ActionResult Details(int id)
        {
            var carViewModel = new CarViewModel(_carService.GetCar(id));
            return PartialView("_Details", carViewModel);
        }

        public ActionResult Edit(int id)
        {
            var carViewModel = new CarViewModel(_carService.GetCar(id));
            return PartialView("_Edit", carViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CarViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", viewModel);
            }

            var car = new Car()
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                Make = viewModel.Make,
                Model = viewModel.Model,
                RegistrationNumber = viewModel.RegistrationNumber,
            };
            _carService.EditUser(car);

            return Json(new { success = true });
        }

        public ActionResult Delete(CarViewModel car)
        {
            _carService.DeleteCar(car.Id);
            return RedirectToAction("Details", "User", new { id = car.UserId });
        }
    }
}
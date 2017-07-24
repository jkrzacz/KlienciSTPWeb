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
            return View(carViewModel);
        }


        [HttpPost]
        public ActionResult Create(CarViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
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
            return RedirectToAction("Details", "User", new { id = viewModel.UserId });
        }

        public ActionResult Details(int id)
        {
            var carViewModel = new CarViewModel(_carService.GetCar(id));
            return View(carViewModel);
        }

        public ActionResult Edit(int id)
        {
            var carViewModel = new CarViewModel(_carService.GetCar(id));
            return View(carViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CarViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
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

            return RedirectToAction("Details", "User", new { id = car.UserId });
        }

        public ActionResult Delete(CarViewModel car)
        {
            _carService.DeleteCar(car.Id);
            return RedirectToAction("Details", "User", new { id = car.UserId });
        }
    }
}
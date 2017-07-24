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
    public class InspectionController : Controller
    {
        private readonly IInspectionService _inspectionService;
        private readonly ICarService _carService;

        public InspectionController()
        {
            _inspectionService = new InspectionService();
            _carService = new CarService();
        }
        public ActionResult Create(int userId)
        {
            var cars = _carService.GetCarsForUser(userId);
            var inspectionViewModel = new InspectionViewModel() {
                UserId = userId,
                InspectionDate = DateTime.Now
            };
            ViewBag.DropListOfCarsToView = GetCarsListView(cars,-1);
            ViewBag.DropListOfYearsToView = GetNextInspectionYearsListView(1);
            return View(inspectionViewModel);
        }


        [HttpPost]
        public ActionResult Create(InspectionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var cars = _carService.GetCarsForUser(viewModel.UserId);
                ViewBag.DropListOfCarsToView = GetCarsListView(cars, -1);
                ViewBag.DropListOfYearsToView = GetNextInspectionYearsListView(1);
                return View(viewModel);
            }

            var inspection = new Inspection()
            {
                Id = viewModel.Id,
                CarId = int.Parse(viewModel.SelectedCarId),
                Comments=viewModel.Comments,
                InspectionDate = viewModel.InspectionDate,
                NextInspectionYears = viewModel.NextInspectionYears
            };

            _inspectionService.CreateInspectionForCar(inspection);
            return RedirectToAction("Details", "User", new { id = viewModel.UserId });
        }

        public ActionResult Edit(int userId,int id)
        {
            var cars = _carService.GetCarsForUser(userId); 
            var inspectionViewModel = new InspectionViewModel(_inspectionService.GetInspection(id), userId);
            ViewBag.DropListOfCarsToView = GetCarsListView(cars,inspectionViewModel.CarId);
            ViewBag.DropListOfYearsToView = GetNextInspectionYearsListView(inspectionViewModel.NextInspectionYears);
            return View(inspectionViewModel);
        }

        [HttpPost]
        public ActionResult Edit(InspectionViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                var cars = _carService.GetCarsForUser(viewModel.UserId);
                ViewBag.DropListOfCarsToView = GetCarsListView(cars, viewModel.CarId);
                ViewBag.DropListOfYearsToView = GetNextInspectionYearsListView(viewModel.NextInspectionYears);
                return View(viewModel);
            }

            var inspection = new Inspection()
            {
                Id = viewModel.Id,
                CarId = int.Parse(viewModel.SelectedCarId),
                Comments = viewModel.Comments,
                InspectionDate = viewModel.InspectionDate,
                NextInspectionYears = viewModel.NextInspectionYears
            };

            _inspectionService.EditInspection(inspection);
            return RedirectToAction("Details", "User", new { id = viewModel.UserId });
        }

        public ActionResult Delete(InspectionViewModel inspection)
        {
            _inspectionService.DeleteInspection(inspection.Id);
            return RedirectToAction("Details", "User", new { id = inspection.UserId });
        }

        public List<SelectListItem> GetCarsListView(List<Car> cars,int carId)
        {
            List<SelectListItem> dropListItems = new List<SelectListItem>();
            foreach (var item in cars)
            {
                if (item.Id == carId)
                {
                    dropListItems.Add(new SelectListItem()
                    {
                        Text = string.Format("{0} {1} ({2})", item.Make, item.Model, item.RegistrationNumber),
                        Value = item.Id.ToString(),
                        Selected = true
                    }
                    );
                }
                else
                {
                    dropListItems.Add(new SelectListItem()
                    {
                        Text = string.Format("{0} {1} ({2})", item.Make, item.Model, item.RegistrationNumber),
                        Value = item.Id.ToString()
                    }
                    );
                }
            }
            return dropListItems;
        }
        public List<SelectListItem> GetNextInspectionYearsListView(int nextInspectionYear)
        {
            List<SelectListItem> dropListItems = new List<SelectListItem>();
            for (int i = 1; i <= 3; i++)
            {
                if (i == nextInspectionYear)
                {
                    dropListItems.Add(new SelectListItem()
                    {
                        Text = i.ToString(),
                        Value = i.ToString(),
                        Selected = true
                    }
                    );
                }
                else
                {
                    dropListItems.Add(new SelectListItem()
                    {
                        Text = i.ToString(),
                        Value = i.ToString()
                    }
                    );
                }
            }
            return dropListItems;
        }
    }
}
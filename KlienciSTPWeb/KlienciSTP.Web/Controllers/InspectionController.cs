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
            return PartialView("_Create", inspectionViewModel);
        }


        [HttpPost]
        public ActionResult Create(InspectionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var cars = _carService.GetCarsForUser(viewModel.UserId);
                ViewBag.DropListOfCarsToView = GetCarsListView(cars, -1);
                ViewBag.DropListOfYearsToView = GetNextInspectionYearsListView(1);
                return PartialView("_Create", viewModel);
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
            return Json(new { success = true });
        }

        public ActionResult Edit(int id, int userId)
        {
            var cars = _carService.GetCarsForUser(userId); 
            var inspectionViewModel = new InspectionViewModel(_inspectionService.GetInspection(id), userId);
            ViewBag.DropListOfCarsToView = GetCarsListView(cars,inspectionViewModel.CarId);
            ViewBag.DropListOfYearsToView = GetNextInspectionYearsListView(inspectionViewModel.NextInspectionYears);
            return PartialView("_Edit", inspectionViewModel);
        }

        [HttpPost]
        public ActionResult Edit(InspectionViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                var cars = _carService.GetCarsForUser(viewModel.UserId);
                ViewBag.DropListOfCarsToView = GetCarsListView(cars, viewModel.CarId);
                ViewBag.DropListOfYearsToView = GetNextInspectionYearsListView(viewModel.NextInspectionYears);
                return PartialView("_Edit", viewModel);
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
            return Json(new { success = true });

        }

        public ActionResult Delete(InspectionViewModel inspection)
        {
            _inspectionService.DeleteInspection(inspection.Id);

            return RedirectToAction("Details", "User", new { id = inspection.UserId });
        }
        public ActionResult DeleteForHistory(InspectionViewModel inspection)
        {
            _inspectionService.DeleteInspection(inspection.Id);
            return RedirectToAction("History", "Inspection", new { carId = inspection.CarId, userId = inspection.UserId });

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
        public ActionResult History(int carId, int userId)
        {
            var inspectionsModel = new List<InspectionViewModel>();
            var inspectionList = _inspectionService.GetInspectionsForCar(carId);
            foreach (var inspection in inspectionList)
            {
                var model = new InspectionViewModel(inspection, userId);
                inspectionsModel.Add(model);
            }

            return View(inspectionsModel);
        }
        public ActionResult FutureInspections()
        {
            var futureNotifications = _inspectionService.GetFutureInspections();
            var futureInspectionsViewModel = new List<NotificationViewModel>();
            foreach (var notification in futureNotifications)
            {
                var model = new NotificationViewModel()
                {
                    CarId = notification.CarId,
                    InspectionId = notification.Id,
                    InspectionDate = notification.InspectionDate.AddYears(notification.NextInspectionYears),
                    Make = notification.Car.Make,
                    Model = notification.Car.Model,
                    Customer = notification.Car.User.LastName + " " + notification.Car.User.FirstName,
                    ContactDetails = "P1: " + notification.Car.User.Phone1 + ",P2: " + notification.Car.User.Phone2 + ",E: " + notification.Car.User.Email,
                    Comment = notification.Comments,
                    RegistrationNumber = notification.Car.RegistrationNumber 
                };
                futureInspectionsViewModel.Add(model);
            }
            return View(futureInspectionsViewModel);
        }
        public ActionResult ClientInspections(int carId)
        {
            var futureNotifications = _inspectionService.GetFutureInspectionsForCar(carId);
            var notificationViewModel = new List<NotificationViewModel>();
            foreach (var notification in futureNotifications)
            {
                var model = new NotificationViewModel()
                {
                    CarId = notification.CarId,
                    InspectionId = notification.Id,
                    InspectionDate = notification.InspectionDate.AddYears(notification.NextInspectionYears),
                    Make = notification.Car.Make,
                    Model = notification.Car.Model,
                    Customer = notification.Car.User.LastName + " " + notification.Car.User.FirstName,
                    ContactDetails = "P1: " + notification.Car.User.Phone1 + ",P2: " + notification.Car.User.Phone2 + ",E: " + notification.Car.User.Email,
                    Comment = notification.Comments,
                    RegistrationNumber = notification.Car.RegistrationNumber
                };
                notificationViewModel.Add(model);
            }
            return View(notificationViewModel);
        }

        public ActionResult MakeNotification(int id, int carId)
        {
            _inspectionService.makeNotification(id);
            if (carId > -1)
            {
                return RedirectToAction("ClientInspections", new { carId = carId });
            }
            return RedirectToAction("FutureInspections");
        }

        public ActionResult InspectionsHistory()
        {
            var notifiedNotifications = _inspectionService.GetHistoryInspections();
            var notificationViewModel = new List<NotificationViewModel>();
            foreach (var notification in notifiedNotifications)
            {
                var model = new NotificationViewModel()
                {
                    NotifiedDate = notification.Notified,
                    CarId = notification.CarId,
                    InspectionId = notification.Id,
                    InspectionDate = notification.InspectionDate.AddYears(notification.NextInspectionYears),
                    Make = notification.Car.Make,
                    Model = notification.Car.Model,
                    Customer = notification.Car.User.LastName + " " + notification.Car.User.FirstName,
                    ContactDetails = "P1: " + notification.Car.User.Phone1 + ",P2: " + notification.Car.User.Phone2 + ",E: " + notification.Car.User.Email,
                    Comment = notification.Comments,
                    RegistrationNumber = notification.Car.RegistrationNumber
                };
                notificationViewModel.Add(model);
            }
            return View(notificationViewModel);
        }
    }
}
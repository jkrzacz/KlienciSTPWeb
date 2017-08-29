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
        public ActionResult History(int carId, int userId, string sortOrder, string searchString)
        {
            var inspectionsModel = new List<InspectionViewModel>();
            var inspectionList = _inspectionService.GetInspectionsForCar(carId);
            foreach (var inspection in inspectionList)
            {
                var model = new InspectionViewModel(inspection, userId);
                inspectionsModel.Add(model);
            }

            ViewBag.SortOrder = sortOrder;
            ViewBag.InspectionDateSortParm = String.IsNullOrEmpty(sortOrder) ? "InspectionDate_desc" : "";
            ViewBag.CommentsSortParm = sortOrder == "Comments" ? "Comments_desc" : "Comments";
            ViewBag.NextInspectionYearsSortParm = sortOrder == "NextInspectionYears" ? "NextInspectionYears_desc" : "NextInspectionYears";

            var inspections = from inspection in inspectionsModel
                        select inspection;
            if (!String.IsNullOrEmpty(searchString))
            {
                inspections = inspections.Where(inspection => (inspection.InspectionDate != null && inspection.InspectionDate.ToString().Contains(searchString))
                                         || (inspection.Comments != null && inspection.Comments.Contains(searchString))
                                         || inspection.NextInspectionYears.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "InspectionDate_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.InspectionDate);
                    break;
                case "Comments":
                    inspections = inspections.OrderBy(inspection => inspection.Comments);
                    break;
                case "Comments_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.Comments);
                    break;
                case "NextInspectionYears":
                    inspections = inspections.OrderBy(inspection => inspection.NextInspectionYears);
                    break;
                case "NextInspectionYears_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.NextInspectionYears);
                    break;
                default:
                    inspections = inspections.OrderBy(inspection => inspection.InspectionDate);
                    break;
            }

            return View(inspections.ToList());
        }
        public ActionResult FutureInspections(string sortOrder, string searchString)
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

            ViewBag.SortOrder = sortOrder;
            ViewBag.InspectionDateSortParm = String.IsNullOrEmpty(sortOrder) ? "InspectionDate_desc" : "";
            ViewBag.MakeSortParm = sortOrder == "Make" ? "Make_desc" : "Make";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.ContactDetailsSortParm = sortOrder == "ContactDetails" ? "ContactDetails_desc" : "ContactDetails";
            ViewBag.CommentSortParm = sortOrder == "Comment" ? "Comment_desc" : "Comment";
            ViewBag.RegistrationNumberSortParm = sortOrder == "RegistrationNumber" ? "RegistrationNumber_desc" : "RegistrationNumber";

            var futureInspections = from futureInspection in futureInspectionsViewModel
                        select futureInspection;
            if (!String.IsNullOrEmpty(searchString))
            {
                futureInspections = futureInspections.Where(futureInspection => (futureInspection.InspectionDate != null && futureInspection.InspectionDate.ToString().Contains(searchString))
                                         || futureInspection.Make.Contains(searchString)
                                         || futureInspection.Model.Contains(searchString)
                                         || futureInspection.Customer.Contains(searchString)
                                         || futureInspection.ContactDetails.Contains(searchString)
                                         || (futureInspection.Comment != null && futureInspection.Comment.Contains(searchString))
                                         || futureInspection.RegistrationNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "InspectionDate_desc":
                    futureInspections = futureInspections.OrderByDescending(futureInspection => futureInspection.InspectionDate);
                    break;
                case "Make":
                    futureInspections = futureInspections.OrderBy(futureInspection => futureInspection.Make);
                    break;
                case "Make_desc":
                    futureInspections = futureInspections.OrderByDescending(futureInspection => futureInspection.Make);
                    break;
                case "Model":
                    futureInspections = futureInspections.OrderBy(futureInspection => futureInspection.Model);
                    break;
                case "Model_desc":
                    futureInspections = futureInspections.OrderByDescending(futureInspection => futureInspection.Model);
                    break;
                case "Customer":
                    futureInspections = futureInspections.OrderBy(futureInspection => futureInspection.Customer);
                    break;
                case "Customer_desc":
                    futureInspections = futureInspections.OrderByDescending(futureInspection => futureInspection.Customer);
                    break;
                case "ContactDetails":
                    futureInspections = futureInspections.OrderBy(futureInspection => futureInspection.ContactDetails);
                    break;
                case "ContactDetails_desc":
                    futureInspections = futureInspections.OrderByDescending(futureInspection => futureInspection.ContactDetails);
                    break;
                case "Comment":
                    futureInspections = futureInspections.OrderBy(futureInspection => futureInspection.Comment);
                    break;
                case "Comment_desc":
                    futureInspections = futureInspections.OrderByDescending(futureInspection => futureInspection.Comment);
                    break;
                case "RegistrationNumber":
                    futureInspections = futureInspections.OrderBy(futureInspection => futureInspection.RegistrationNumber);
                    break;
                case "RegistrationNumber_desc":
                    futureInspections = futureInspections.OrderByDescending(futureInspection => futureInspection.RegistrationNumber);
                    break;
                default:
                    futureInspections = futureInspections.OrderBy(futureInspection => futureInspection.InspectionDate);
                    break;
            }

            return View(futureInspections.ToList());
        }
        public ActionResult ClientInspections(int carId, string sortOrder, string searchString)
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

            ViewBag.SortOrder = sortOrder;
            ViewBag.InspectionDateSortParm = String.IsNullOrEmpty(sortOrder) ? "InspectionDate_desc" : "";
            ViewBag.MakeSortParm = sortOrder == "Make" ? "Make_desc" : "Make";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.ContactDetailsSortParm = sortOrder == "ContactDetails" ? "ContactDetails_desc" : "ContactDetails";
            ViewBag.CommentSortParm = sortOrder == "Comment" ? "Comment_desc" : "Comment";
            ViewBag.RegistrationNumberSortParm = sortOrder == "RegistrationNumber" ? "RegistrationNumber_desc" : "RegistrationNumber";

            var inspections = from inspection in notificationViewModel
                                    select inspection;
            if (!String.IsNullOrEmpty(searchString))
            {
                inspections = inspections.Where(inspection => (inspection.InspectionDate != null && inspection.InspectionDate.ToString().Contains(searchString))
                                         || inspection.Make.Contains(searchString)
                                         || inspection.Model.Contains(searchString)
                                         || inspection.Customer.Contains(searchString)
                                         || inspection.ContactDetails.Contains(searchString)
                                         || (inspection.Comment != null && inspection.Comment.Contains(searchString))
                                         || inspection.RegistrationNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "InspectionDate_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.InspectionDate);
                    break;
                case "Make":
                    inspections = inspections.OrderBy(inspection => inspection.Make);
                    break;
                case "Make_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.Make);
                    break;
                case "Model":
                    inspections = inspections.OrderBy(inspection => inspection.Model);
                    break;
                case "Model_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.Model);
                    break;
                case "Customer":
                    inspections = inspections.OrderBy(inspection => inspection.Customer);
                    break;
                case "Customer_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.Customer);
                    break;
                case "ContactDetails":
                    inspections = inspections.OrderBy(inspection => inspection.ContactDetails);
                    break;
                case "ContactDetails_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.ContactDetails);
                    break;
                case "Comment":
                    inspections = inspections.OrderBy(inspection => inspection.Comment);
                    break;
                case "Comment_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.Comment);
                    break;
                case "RegistrationNumber":
                    inspections = inspections.OrderBy(inspection => inspection.RegistrationNumber);
                    break;
                case "RegistrationNumber_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.RegistrationNumber);
                    break;
                default:
                    inspections = inspections.OrderBy(inspection => inspection.InspectionDate);
                    break;
            }

            return View(inspections.ToList());
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

        public ActionResult InspectionsHistory(string sortOrder, string searchString)
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

            ViewBag.SortOrder = sortOrder;
            ViewBag.NotifiedDateSortParm = String.IsNullOrEmpty(sortOrder) ? "NotifiedDate_desc" : "";
            ViewBag.InspectionDateSortParm = sortOrder == "InspectionDate" ? "InspectionDate_desc" : "InspectionDate";
            ViewBag.MakeSortParm = sortOrder == "Make" ? "Make_desc" : "Make";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.ContactDetailsSortParm = sortOrder == "ContactDetails" ? "ContactDetails_desc" : "ContactDetails";
            ViewBag.CommentSortParm = sortOrder == "Comment" ? "Comment_desc" : "Comment";
            ViewBag.RegistrationNumberSortParm = sortOrder == "RegistrationNumber" ? "RegistrationNumber_desc" : "RegistrationNumber";

            var Inspections = from Inspection in notificationViewModel
                                    select Inspection;
            if (!String.IsNullOrEmpty(searchString))
            {
                Inspections = Inspections.Where(Inspection => Inspection.NotifiedDate.ToString().Contains(searchString)
                                         || (Inspection.InspectionDate != null && Inspection.InspectionDate.ToString().Contains(searchString))
                                         || Inspection.Make.Contains(searchString)
                                         || Inspection.Model.Contains(searchString)
                                         || Inspection.Customer.Contains(searchString)
                                         || Inspection.ContactDetails.Contains(searchString)
                                         || (Inspection.Comment != null && Inspection.Comment.Contains(searchString))
                                         || Inspection.RegistrationNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "NotifiedDate_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.NotifiedDate);
                    break;
                case "InspectionDate":
                    Inspections = Inspections.OrderBy(Inspection => Inspection.InspectionDate);
                    break;
                case "InspectionDate_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.InspectionDate);
                    break;
                case "Make":
                    Inspections = Inspections.OrderBy(Inspection => Inspection.Make);
                    break;
                case "Make_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.Make);
                    break;
                case "Model":
                    Inspections = Inspections.OrderBy(Inspection => Inspection.Model);
                    break;
                case "Model_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.Model);
                    break;
                case "Customer":
                    Inspections = Inspections.OrderBy(Inspection => Inspection.Customer);
                    break;
                case "Customer_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.Customer);
                    break;
                case "ContactDetails":
                    Inspections = Inspections.OrderBy(Inspection => Inspection.ContactDetails);
                    break;
                case "ContactDetails_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.ContactDetails);
                    break;
                case "Comment":
                    Inspections = Inspections.OrderBy(Inspection => Inspection.Comment);
                    break;
                case "Comment_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.Comment);
                    break;
                case "RegistrationNumber":
                    Inspections = Inspections.OrderBy(Inspection => Inspection.RegistrationNumber);
                    break;
                case "RegistrationNumber_desc":
                    Inspections = Inspections.OrderByDescending(Inspection => Inspection.RegistrationNumber);
                    break;
                default:
                    Inspections = Inspections.OrderBy(Inspection => Inspection.NotifiedDate);
                    break;
            }

            return View(Inspections.ToList());
        }
    }
}
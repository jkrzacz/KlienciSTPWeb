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
        private readonly ICarService _carService;
        private readonly IInspectionService _inspectionService;

        public UserController()
        {
            _userService = new UserService();
            _carService = new CarService();
            _inspectionService = new InspectionService();
        }


        // GET: User
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userList = _userService.GetUsers();
            var usersModel = new List<UserViewModel>();

            foreach (var user in userList)
            {
                var model = new UserViewModel(user);
                usersModel.Add(model);
            }

            ViewBag.SortOrder = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.Phone1SortParm = sortOrder == "Phone1" ? "Phone1_desc" : "Phone1";
            ViewBag.Phone2SortParm = sortOrder == "Phone2" ? "Phone2_desc" : "Phone2";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";

            var users = from user in usersModel
                        select user;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(user => user.FirstName.Contains(searchString)
                                         || user.LastName.Contains(searchString)
                                         || user.Phone1.Contains(searchString)
                                         || (user.Phone2 != null && user.Phone2.Contains(searchString))
                                         || (user.Email != null && user.Email.Contains(searchString)));
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
                case "Phone1":
                    users = users.OrderBy(user => user.Phone1);
                    break;
                case "Phone1_desc":
                    users = users.OrderByDescending(user => user.Phone1);
                    break;
                case "Phone2":
                    users = users.OrderBy(user => user.Phone2);
                    break;
                case "Phone2_desc":
                    users = users.OrderByDescending(user => user.Phone2);
                    break;
                case "Email":
                    users = users.OrderBy(user => user.Email);
                    break;
                case "Email_desc":
                    users = users.OrderByDescending(user => user.Email);
                    break;
                default:
                    users = users.OrderBy(user => user.FirstName);
                    break;
            }

            return View(users.ToList());
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", model);
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

            return Json(new { success = true });
        }

        public ActionResult Details(int id, string carSortOrder, string carSearchString, string inspectionSortOrder, string inspectionSearchString)
        {
            var userModel = new UserViewModel(_userService.GetUser(id));
            var carsModel = new List<CarViewModel>();
            var inspectionsModel = new List<InspectionViewModel>();
            var carList = _carService.GetCarsForUser(userModel.Id);
            var inspectionList = _inspectionService.GetInspectionsForUser(userModel.Id);

            foreach (var car in carList)
            {
                var model = new CarViewModel(car);
                carsModel.Add(model);
            }

            ViewBag.CarSortOrder = carSortOrder;
            ViewBag.CarIdSortParm = String.IsNullOrEmpty(carSortOrder) ? "Id_desc" : "";
            ViewBag.CarMakeSortParm = carSortOrder == "Make" ? "Make_desc" : "Make";
            ViewBag.CarModelSortParm = carSortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.CarRegistrationNumberSortParm = carSortOrder == "RegistrationNumber" ? "RegistrationNumber_desc" : "RegistrationNumber";

            var cars = from car in carsModel
                       select car;
            if (!String.IsNullOrEmpty(carSearchString))
            {
                cars = cars.Where(car => car.Id.ToString().Contains(carSearchString)
                                         || car.Make.Contains(carSearchString)
                                         || car.Model.Contains(carSearchString)
                                         || car.RegistrationNumber.Contains(carSearchString));
            }
            switch (carSortOrder)
            {
                case "Id_desc":
                    cars = cars.OrderByDescending(car => car.Id);
                    break;
                case "Make":
                    cars = cars.OrderBy(car => car.Make);
                    break;
                case "Make_desc":
                    cars = cars.OrderByDescending(car => car.Make);
                    break;
                case "Model":
                    cars = cars.OrderBy(car => car.Model);
                    break;
                case "Model_desc":
                    cars = cars.OrderByDescending(car => car.Model);
                    break;
                case "RegistrationNumber":
                    cars = cars.OrderBy(car => car.RegistrationNumber);
                    break;
                case "RegistrationNumber_desc":
                    cars = cars.OrderByDescending(car => car.RegistrationNumber);
                    break;
                default:
                    cars = cars.OrderBy(car => car.Id);
                    break;
            }

            foreach (var inspection in inspectionList)
            {
                var model = new InspectionViewModel(inspection,id);
                inspectionsModel.Add(model);
            }

            ViewBag.InspectionSortOrder = inspectionSortOrder;
            ViewBag.InspectionCarIdSortParm = String.IsNullOrEmpty(inspectionSortOrder) ? "CarId_desc" : "";
            ViewBag.InspectionInspectionDateSortParm = inspectionSortOrder == "InspectionDate" ? "InspectionDate_desc" : "InspectionDate";
            ViewBag.InspectionCommentsSortParm = inspectionSortOrder == "Comments" ? "Comments_desc" : "Comments";
            ViewBag.InspectionNextInspectionYearsSortParm = inspectionSortOrder == "NextInspectionYears" ? "NextInspectionYears_desc" : "NextInspectionYears";

            var inspections = from inspection in inspectionsModel
                              select inspection;
            if (!String.IsNullOrEmpty(inspectionSearchString))
            {
                inspections = inspections.Where(inspection => inspection.CarId.ToString().Contains(inspectionSearchString)
                                         || (inspection.InspectionDate != null && inspection.InspectionDate.ToString().Contains(inspectionSearchString))
                                         || (inspection.Comments != null && inspection.Comments.Contains(inspectionSearchString))
                                         || inspection.NextInspectionYears.ToString().Contains(inspectionSearchString));
            }
            switch (inspectionSortOrder)
            {
                case "CarId_desc":
                    inspections = inspections.OrderByDescending(inspection => inspection.CarId);
                    break;
                case "InspectionDate":
                    inspections = inspections.OrderBy(inspection => inspection.InspectionDate);
                    break;
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
                    inspections = inspections.OrderBy(inspection => inspection.CarId);
                    break;
            }

            var userWithCarViewModel = new UserWithCarViewModel() {
                User = userModel,
                Cars = cars.ToList(),
                Inspections = inspections.ToList()
            };
            return View(userWithCarViewModel);
        }

        public ActionResult Edit(int id)
        {
            var userViewModel = new UserViewModel(_userService.GetUser(id));
            return PartialView("_Edit", userViewModel);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", model);
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

            return Json(new { success = true });
        }

        public ActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
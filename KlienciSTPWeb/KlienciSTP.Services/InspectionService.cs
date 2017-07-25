using KlienciSTP.Data.Model;
using KlienciSTP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlienciSTP.Services
{
    public class InspectionService : BaseService, IInspectionService
    {
        public List<Inspection> GetInspectionsForCar(int carId)
        {
            return _dbContext.Inspection.Where(u => u.Deleted == null && u.CarId == carId).ToList();
        }       

        public Inspection GetInspection(int id)
        {
            return _dbContext.Inspection.FirstOrDefault(u => u.Deleted == null && u.Id == id);
        }

        public void CreateInspectionForCar(Inspection inspection)
        {
            inspection.Created = DateTime.Now;
            _dbContext.Inspection.Add(inspection);
            _dbContext.SaveChanges();
        }

        public void DeleteInspection(int idInspection)
        {
            var inspection = _dbContext.Inspection.FirstOrDefault(u => u.Deleted == null && u.Id == idInspection);
            if (inspection != null)
            {
                inspection.Deleted = DateTime.Now;
                _dbContext.SaveChanges();
            }
        }

        public void EditInspection(Inspection dane)
        {
            var inspection = _dbContext.Inspection.FirstOrDefault(u => u.Deleted == null && u.Id == dane.Id);
            if (inspection != null)
            {
                if (dane.CarId > 0)
                {
                    inspection.CarId = dane.CarId;
                }
                inspection.InspectionDate = dane.InspectionDate;
                inspection.Comments = dane.Comments;
                inspection.NextInspectionYears = dane.NextInspectionYears;
                _dbContext.SaveChanges();
            }
        }

        public List<Inspection> GetInspectionsForUser(int userId)
        {
            var carList = _dbContext.Car.Where(u => u.Deleted == null && u.UserId == userId).ToList();
            List<Inspection> inspectionList = new List<Inspection>();
            foreach (var element in carList)
            {
                inspectionList.AddRange(GetInspectionsForCar(element.Id));
            }
            return inspectionList;
        }
        public List<Inspection> GetFutureInspections()
        {
            int notificationPeriod = 14;
            var notDeletedNotification = _dbContext.Inspection.Where(u => u.Deleted == null && u.Notified == null).ToList();
            var futureNotification = notDeletedNotification.Where(u => u.InspectionDate.AddYears(u.NextInspectionYears).AddDays(-1 * notificationPeriod) <= DateTime.Today).ToList();
            return futureNotification;
        }
        public List<Inspection> GetHistoryInspections()
        {
            var notifiedNotifications = _dbContext.Inspection.Where(u => u.Deleted == null && u.Notified != null).ToList();
            return notifiedNotifications;
        }

        public List<Inspection> GetFutureInspectionsForCar(int carId2)
        {
            var notDeletedNotification = _dbContext.Inspection.Where(u => u.Deleted == null && u.Notified == null && u.CarId == carId2).ToList();
            return notDeletedNotification;
        }
        public void makeNotification(int idInspection)
        {
            var inspection = _dbContext.Inspection.FirstOrDefault(u => u.Deleted == null && u.Id == idInspection);
            if (inspection != null)
            {
                inspection.Notified = DateTime.Now;
                _dbContext.SaveChanges();
            }
        }
    }
}

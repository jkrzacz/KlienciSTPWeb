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
                inspection.CarId = dane.CarId;
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
    }
}

using KlienciSTP.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlienciSTP.Interfaces
{
    public interface IInspectionService
    {
        List<Inspection> GetInspectionsForCar(int carId);

        Inspection GetInspection(int id);

        void CreateInspectionForCar(Inspection inspection);

        void DeleteInspection(int idInspection);

        void EditInspection(Inspection dane);

        List<Inspection> GetInspectionsForUser(int userId);
        List<Inspection> GetFutureInspections();
        List<Inspection> GetHistoryInspections();
        List<Inspection> GetFutureInspectionsForCar(int carId);
        void makeNotification(int id);
    }
}

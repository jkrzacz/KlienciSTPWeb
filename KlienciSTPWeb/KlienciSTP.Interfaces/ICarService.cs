using KlienciSTP.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlienciSTP.Interfaces
{
    public interface ICarService
    {
        List<Car> GetCarsForUser(int userId);
        Car GetCar(int id);
        void CreateCarForUser(Car car);
        void DeleteCar(int idCar);
        void EditUser(Car dane);
    }
}

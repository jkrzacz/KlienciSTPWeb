using KlienciSTP.Data.Model;
using KlienciSTP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlienciSTP.Services
{
    public class CarService : BaseService, ICarService
    {
        public List<Car> GetCarsForUser(int userId)
        {
            return _dbContext.Car.Where(u => u.Deleted == null && u.UserId == userId).ToList();
        }

        public Car GetCar(int id)
        {
            return _dbContext.Car.FirstOrDefault(u => u.Deleted == null && u.Id == id);
        }

        public void CreateCarForUser(Car car)
        {
            car.Created = DateTime.Now;
            _dbContext.Car.Add(car);
            _dbContext.SaveChanges();
        }

        public void DeleteCar(int idCar)
        {
            var car = _dbContext.Car.FirstOrDefault(u => u.Deleted == null && u.Id == idCar);
            if (car != null)
            {
                car.Deleted = DateTime.Now;
                _dbContext.SaveChanges();
            }
        }

        public void EditUser(Car dane)
        {
            var car = _dbContext.Car.FirstOrDefault(u => u.Deleted == null && u.Id == dane.Id);
            if (car != null)
            {
                car.Make = dane.Make;
                car.Model = dane.Model;
                car.RegistrationNumber = dane.RegistrationNumber;
                _dbContext.SaveChanges();
            }
        }
    }
}

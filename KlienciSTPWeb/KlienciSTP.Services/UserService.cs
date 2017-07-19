using KlienciSTP.Data.Model;
using KlienciSTP.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KlienciSTP.Services
{
    public class UserService : BaseService, IUserService
    {
        public List<User> GetUsers()
        {
            return _dbContext.User.Where(u => u.Deleted == null).ToList();
        }

        public User GetUser(int id)
        {
            return _dbContext.User.FirstOrDefault(u => u.Deleted == null && u.Id == id);
        }

        public void CreateUser(User user)
        {
            user.Created = DateTime.Now;
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int idUser)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Deleted == null && u.Id == idUser);
            if (user != null)
            {
                user.Deleted = DateTime.Now;
                _dbContext.SaveChanges();
            }
        }

        public void EditUser(User dane)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Deleted == null && u.Id == dane.Id);
            if (user != null)
            {
                user.FirstName = dane.FirstName;
                user.LastName = dane.LastName;
                user.Phone1 = dane.Phone1;
                user.Phone2 = dane.Phone2;
                user.Email = dane.Email;
                _dbContext.SaveChanges();
            }
        }
    }
}

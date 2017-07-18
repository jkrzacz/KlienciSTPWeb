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
    }
}

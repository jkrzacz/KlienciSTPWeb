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
            return _dbContext.User.ToList();
        }

        public void CreateUser(User user)
        {
            user.Created = DateTime.Now;
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }
    }
}

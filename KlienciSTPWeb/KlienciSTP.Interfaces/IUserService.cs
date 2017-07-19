using KlienciSTP.Data.Model;
using System.Collections.Generic;

namespace KlienciSTP.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        void CreateUser(User user);
        void DeleteUser(int idUser);
        void EditUser(User user);
    }
}

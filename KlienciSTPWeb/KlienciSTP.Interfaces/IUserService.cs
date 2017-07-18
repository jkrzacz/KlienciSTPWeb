using KlienciSTP.Data.Model;
using System.Collections.Generic;

namespace KlienciSTP.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        void CreateUser(User user);
    }
}

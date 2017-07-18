using KlienciSTP.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlienciSTP.Services
{
    public abstract class BaseService
    {
        protected readonly DBKlienciSTPEntities _dbContext;

        public BaseService()
        {
            _dbContext = new DBKlienciSTPEntities();
        }
    }
}

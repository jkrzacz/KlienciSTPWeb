using KlienciSTP.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlienciSTP.Web.Models
{
    public class InspectionWithCarsModel
    {
        public InspectionViewModel Inspection { get; set; }
        public List<Car> Cars { get; set; }
        public int UserId { get; set; }
    }
}
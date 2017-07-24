using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlienciSTP.Web.Models
{
    public class UserWithCarViewModel
    {
        public UserViewModel User { get; set; }
        public List<CarViewModel> Cars { get; set; }
        public List<InspectionViewModel> Inspections { get; set; }
    }
}
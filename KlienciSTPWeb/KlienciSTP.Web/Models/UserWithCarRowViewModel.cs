using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KlienciSTP.Web.Models
{
    public class UserWithCarRowViewModel
    {

        public int UserId { get; set; }

        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        public int CarId { get; set; }

        [DisplayName("Marka")]
        public string Make { get; set; }


        [DisplayName("Model")]
        public string Model { get; set; }

        [DisplayName("Numer Rejestracyjny")]
        public string RegistrationNumber { get; set; }
    }
}
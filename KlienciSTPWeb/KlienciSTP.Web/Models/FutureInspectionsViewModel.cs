using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlienciSTP.Web.Models
{
    public class NotificationViewModel
    {
        public int InspectionId { get; set; }

        public int CarId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data powiadomienia")]
        public Nullable<System.DateTime> NotifiedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data planowanego przeglądu")]
        public System.DateTime InspectionDate { get; set; }

        [DisplayName("Marka")]
        public string Make { get; set; }

        [DisplayName("Model")]
        public string Model { get; set; }

        [DisplayName("Klient")]
        public string Customer { get; set; }

        [DisplayName("Dane kontaktowe")]
        public string ContactDetails { get; set; }

        [DisplayName("Komentarz przeglądu")]
        public string Comment { get; set; }

        [DisplayName("Numer Rejestracyjny")]
        public string RegistrationNumber { get; set; }
    }
}
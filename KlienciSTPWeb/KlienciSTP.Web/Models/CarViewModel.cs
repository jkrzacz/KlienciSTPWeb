using KlienciSTP.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlienciSTP.Web.Models
{
    public class CarViewModel
    {
        public CarViewModel() { }

        public CarViewModel(Car car)
        {
            Id = car.Id;
            UserId = car.UserId;
            Make = car.Make;
            Model = car.Model;
            RegistrationNumber = car.RegistrationNumber;
            
        }
        [DisplayName("Identyfikator")]
        public int Id { get; set; }

        [DisplayName("Id Użytkownika")]
        public int UserId { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("Marka")]
        public string Make { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("Model")]
        public string Model { get; set; }
        [Required]
        [StringLength(8)]
        [DisplayName("Numer Rejestracyjny")]
        public string RegistrationNumber { get; set; }
    }
}
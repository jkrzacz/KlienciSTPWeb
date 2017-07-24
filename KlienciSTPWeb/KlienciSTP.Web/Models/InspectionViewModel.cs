using KlienciSTP.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KlienciSTP.Web.Models
{
    public class InspectionViewModel
    {
        public InspectionViewModel() { }

        public InspectionViewModel(Inspection inspection,int userId)
        {
            
            Id = inspection.Id;
            CarId = inspection.CarId;
            UserId= userId;
            InspectionDate = inspection.InspectionDate;
            Comments = inspection.Comments;
            NextInspectionYears = inspection.NextInspectionYears;
            
        }

        public int Id { get; set; }

        [DisplayName("Id Samochodu")]
        public int CarId { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data przeglądu")]
        public System.DateTime InspectionDate { get; set; }

        [StringLength(255)]
        [DisplayName("Komentarz")]
        public string Comments { get; set; }

        [Required]
        [DisplayName("Okres przeglądu")]
        public int NextInspectionYears { get; set; }

        [DisplayName("Samochód")]
        public string SelectedCarId { get; set; }
    }
}
using KlienciSTP.Data.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KlienciSTP.Web.Models
{
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Phone1 = user.Phone1;
            Phone2 = user.Phone2;
            Email = user.Email;
            Created = user.Created;
        }

        [DisplayName("Indeks")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Numer telefonu")]
        public string Phone1 { get; set; }

        [DisplayName("Pomocniczy numer telefonu")]
        public string Phone2 { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("DataUtworzenia")]
        public System.DateTime? Created { get; set; }
    }
}
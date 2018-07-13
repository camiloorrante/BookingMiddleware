using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingMiddleware.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        public string ClientName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El apellido del cliente es obligatorio")]
        public string ClientLastName { get; set; }

        [Display(Name = "Correo")]
        [EmailAddress]
        [Required(ErrorMessage = "El email del cliente es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [Display(Name = "Duración")]
        public int Duration { get; set; }

        
        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
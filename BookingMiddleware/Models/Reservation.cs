using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingMiddleware.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string ClientName { get; set; }
        public string ClientLastName { get; set; }
        public string Email { get; set; }
        public TimeSpan Duration { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }
    }
}
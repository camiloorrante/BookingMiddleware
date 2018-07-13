using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingMiddleware.Models
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public ICollection<City> Cities { get; set; }
        public WeatherResponse WeatherResponse { get; set; }
    }
}
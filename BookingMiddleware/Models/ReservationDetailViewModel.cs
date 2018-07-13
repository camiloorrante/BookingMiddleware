using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingMiddleware.Models
{
    public class ReservationDetailViewModel
    {
        public Reservation Reservation { get; set; }
        public WeatherResponse WeatherResponse { get; set; }
    }
}
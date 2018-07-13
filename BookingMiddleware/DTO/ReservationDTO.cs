using BookingMiddleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingMiddleware.DTO
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }       
        public string ClientName { get; set; }
        public string ClientLastName { get; set; }
        public string Email { get; set; }
        public int Duration { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
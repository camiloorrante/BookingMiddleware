using BookingMiddleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingMiddleware.DTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        public int apiId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
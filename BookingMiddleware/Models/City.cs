﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingMiddleware.Models
{
    public class City
    {
        public int Id { get; set; }
        public int apiId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}